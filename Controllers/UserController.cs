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

            var books = await _context.Books.ToListAsync(); // Get all books from the database
            
            var viewModel = new UserDashboardViewModel
            {
                Username = user.UserName,
                Email = user.Email,
                Books = books // Assign books to the view model
            };

            return View(viewModel);
        }
    }
}
