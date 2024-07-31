using Customer_manager.Models;
using Customer_manager.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Customer_manager.Controllers
{
    public class PortfolyoController:Controller
    {

        private readonly UserManager<Customers> _userManager;
        private readonly GenericRepository<Portfolyo> _repository;
        private readonly ApplicationDbContext _context;

        public PortfolyoController(GenericRepository<Portfolyo> repository, UserManager<Customers> userManager, ApplicationDbContext context)
        {
            _repository = repository;
            _userManager = userManager;
            _context = context;


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
            var model = new Portfolyo();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Portfolyo entity)
        {
            var photoPath = await PhotoCreateQuery(entity);
            if (photoPath != null)
            {
                entity.PhotoPath = photoPath;
                ModelState.Clear();
                TryValidateModel(entity);
            }

            if (ModelState.IsValid)
            {
               
                int customerInfoId = GetLoggedInCustomerInfoId();
                entity.CustomerInfoId = customerInfoId;

                // Veritabanına ekle
                _repository.Insert(entity);

                return RedirectToAction("Index", "Admin");
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
        public async Task<ActionResult> Edit(Portfolyo entity)
        {
            if (ModelState.IsValid)
            {
				await PhotoCreateQuery(entity);
				int customerInfoId = GetLoggedInCustomerInfoId();
				
				entity.CustomerInfoId = customerInfoId;
				
                _repository.Update(entity);
                return RedirectToAction("Index", "Admin");
            }
            return View(entity);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var entity = _repository.GetById(id);
            _repository.Delete(entity);
            return RedirectToAction("Index", "Admin");
        }



        private int GetLoggedInCustomerInfoId()
        {

            string loggedInUserName = User.Identity.Name;

            var customerInfo = _context.CustomerInfos
                .FirstOrDefault(ci => ci.Customer.CustomerKey == loggedInUserName);

            return customerInfo?.CustomerInfoId ?? 0;

        }

        public static async Task<string> PhotoCreateQuery(Portfolyo entity)
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
