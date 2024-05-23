using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ebookings.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization; // Add this namespace

namespace ebookings.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        // GET: /User/Dashboard
        [Authorize] // Add this attribute to require authentication
        public async Task<IActionResult> Dashboard()
        {
            // Get the logged-in user using ASP.NET Core Identity UserManager
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                // Redirect to login if user is not authenticated
                return RedirectToAction("Login", "Account");
            }

            // Create a view model and pass the user details to the view
            var viewModel = new UserDashboardViewModel
            {
                Username = user.UserName,
                Email = user.Email
            };

            return View(viewModel); // Renders the Dashboard.cshtml view with user details
        }

        // Other actions like Profile, Orders, etc.
    }
}
