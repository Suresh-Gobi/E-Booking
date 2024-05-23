using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ebookings.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

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
        [AllowAnonymous] // Allow anonymous access to this action
        [ValidateAntiForgeryToken] // Add anti-forgery token for security
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                // Find the user using their email
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    // Sign in the user using SignInManager
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        // Redirect to dashboard on successful login
                        return RedirectToAction("Dashboard", "User");
                    }
                }

                // If login fails, add an error to ModelState
                ModelState.AddModelError(string.Empty, "Invalid login attempt");
            }

            // Return the view with the model (and any validation errors)
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
