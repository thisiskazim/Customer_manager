using Customer_manager.Models;
using Customer_manager.Repositories;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;



namespace Customer_manager.Controllers
{
	public class AboutController:Controller
	{
		private readonly UserManager<Customers> _userManager;
		private readonly GenericRepository<About> _repository;
		private readonly ApplicationDbContext _context;
		private readonly IWebHostEnvironment _webHostEnvironment;
		public AboutController(IWebHostEnvironment webHostEnvironment, GenericRepository<About> repository, UserManager<Customers> userManager, ApplicationDbContext context)
		{
			_repository = repository;
			_userManager = userManager;
			_context = context;
			_webHostEnvironment = webHostEnvironment;

		}

		public ActionResult Index()
		{
			// İçine Hizmetler listesini ekleyin
			var customerInfo = _repository.GetAll();

			return View(customerInfo);
		}
        [HttpGet]
        public IActionResult Create()
        {
            var model = new About();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(About entity)
        {

            var photoPath = await PhotoCreateQuery(entity);
            if (photoPath != null)
            {
                entity.PhotoPath = photoPath;

                // ModelState'i manuel olarak temizle ve yeniden doğrula
                ModelState.Clear();
                TryValidateModel(entity);
                
            }



            if (ModelState.IsValid)
            {
                int customerInfoId = GetLoggedInCustomerInfoId();
                entity.CustomerInfoId = customerInfoId;
                // Veritabanında mevcut kaydı güncelleyin
                _repository.Insert(entity);
                return RedirectToAction("Index", "Admin");
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }

            return View(entity);
        }

        [HttpGet]
		public ActionResult Edit(int id)
		{

			var entity = _repository.GetById(id);
			return View(entity);
		}

        [HttpPost]
        public async Task<ActionResult> Edit(About entity)
        {
            if (ModelState.IsValid)
            {
                
                await PhotoCreateQuery(entity);


                // Veritabanında mevcut kaydı güncelleyin
                _repository.Update(entity);
                return RedirectToAction("Index", "Admin");
            }
            return View(entity);
        }

        private int GetLoggedInCustomerInfoId()
        {

            string loggedInUserName = User.Identity.Name;

            var customerInfo = _context.CustomerInfos
                .FirstOrDefault(ci => ci.Customer.CustomerKey == loggedInUserName);

            return customerInfo?.CustomerInfoId ?? 0;

        }

        public static  async Task<string>  PhotoCreateQuery(About entity)
        {
            if (entity.Photo != null && entity.Photo.Length > 0)
            {
                // Dosya adını oluşturun
                var fileName = Path.GetFileName(entity.Photo.FileName);

                // Dosya yolunu oluşturun
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

                // Hedef dizini oluşturun
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, fileName);

                // Dosyayı kaydedin
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await entity.Photo.CopyToAsync(stream);
                }

                // Dosya yolunu veritabanına kaydedin
                entity.PhotoPath = "/images/" + fileName;
              
            }
            return entity.PhotoPath;
        }
    }
}
