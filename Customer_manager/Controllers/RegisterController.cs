using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Customer_manager.Models;


namespace Customer_manager.Controllers
{
	public class RegisterController : Controller
	{

        private readonly SignInManager<Customers> _signInManager;
        private readonly UserManager<Customers> _userManager;
        private readonly ApplicationDbContext _context;
		private readonly IPasswordHasher<Customers> _passwordHasher;

		public RegisterController(ApplicationDbContext context, IPasswordHasher<Customers> passwordHasher,SignInManager<Customers> signInManager, UserManager<Customers> userManager)
		{
			_signInManager = signInManager;
			_context = context;
			_passwordHasher = passwordHasher;
			_userManager = userManager;

		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

    
        [HttpPost]
        public async Task<IActionResult> Index(Customers customer, string password)
        {
            if (ModelState.IsValid)
            {
                customer.UserName = customer.CustomerKey;
           
                var result = await _userManager.CreateAsync(customer, password);

                if (result.Succeeded)
                {

                    var customerInfo = new Customer_infos
                    {
                        CustomerKey = customer.CustomerKey,
                        
                   
                    };

                    _context.CustomerInfos.Add(customerInfo);
                    await _context.SaveChangesAsync();

                  

                    return RedirectToAction("LoginIndex", "Register");
                }

               
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(customer);
        }



        
        [HttpGet]
		public IActionResult LoginIndex()
		{
			return View();
		}


        [HttpPost]
        public async Task<IActionResult> LoginIndex(string customerKey, string password)
        {
            if (ModelState.IsValid)

            {
                var customer = await _userManager.FindByNameAsync(customerKey);

                if (customer != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(customerKey, password, isPersistent: true, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (result.IsLockedOut)
                    {
                        return View();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid login attempt.");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("", $"User '{customerKey}' not found.");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

    }
}
