using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ebookings.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ebookings.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(UserSignupModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Username, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    _context.UserSignupModels.Add(model);
                    await _context.SaveChangesAsync(); // Save changes to the database

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var userSignup = await _context.UserSignupModels.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (userSignup != null && userSignup.Password == model.Password)
                {
                    // You can perform additional checks here if needed
                    // For example, checking if the user account is active or verified

                    // Successful login, you may implement your own logic here
                    return RedirectToAction("Dashboard", "Account"); // Redirect to dashboard on successful login
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt");
                    return View(model);
                }
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home"); // Redirect to home page after logout
        }
    }
}
