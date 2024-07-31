using Customer_manager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Customer_manager.Controllers
{
	public class HomeController : Controller
	{

        private readonly UserManager<Customers> _userManager;
        private readonly ApplicationDbContext _context;

        public HomeController(UserManager<Customers> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;

        }   
          public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
          
                var customerInfo = await _context.CustomerInfos
              .Include(ci => ci.About)
              .Include(ci => ci.Contact)
              .Include(ci => ci.Hizmetler)
              .Include(ci => ci.Ozgecmis)
              .Include(ci => ci.Portfolyo)
              .Include(ci => ci.Skills)
              .Include(ci => ci.SSS)
              .FirstOrDefaultAsync(ci => ci.CustomerKey == user.CustomerKey);

                if (customerInfo != null)
                {
                 

                    return View(customerInfo);
                }
            }

            return View();
        }


    }
}
