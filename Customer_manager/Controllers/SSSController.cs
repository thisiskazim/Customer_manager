using Customer_manager.Models;
using Customer_manager.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Customer_manager.Controllers
{
    public class SSSController:Controller
    {
        private readonly UserManager<Customers> _userManager;
        private readonly GenericRepository<SSS> _repository;
        private readonly ApplicationDbContext _context;

        public SSSController(GenericRepository<SSS> repository, UserManager<Customers> userManager, ApplicationDbContext context)
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
            var model = new SSS();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(SSS entity)
        {
            if (ModelState.IsValid)
            {
                // Giriş yapan kullanıcının CustomerInfoId'sini al
                int customerInfoId = GetLoggedInCustomerInfoId();

                if (customerInfoId == 0)
                {
                    // Eğer kullanıcı bulunamazsa hata döndürülebilir
                    ModelState.AddModelError("", "Kullanıcı bilgileri bulunamadı.");
                    return View(entity);
                }

                // Hizmetler nesnesine CustomerInfoId'yi ekle
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
        public ActionResult Edit(SSS entity)
        {
            if (ModelState.IsValid)
            {
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

    }
}
