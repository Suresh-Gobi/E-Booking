using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ebookings.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace ebookings.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public UserController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: /User/Dashboard
        [Authorize]
        public async Task<IActionResult> Dashboard()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var books = await _context.Books.ToListAsync();
            
            var viewModel = new UserDashboardViewModel
            {
                Username = user.UserName,
                Email = user.Email,
                Books = books,
                Cart = new CartItem() // Initialize the Cart for the user
            };

            return View(viewModel);
        }

        // GET: /User/All
        public async Task<IActionResult> All()
        {
            var users = await _userManager.Users.ToListAsync();
            return View("Views/Admin/All.cshtml", users);
        }
    }
}