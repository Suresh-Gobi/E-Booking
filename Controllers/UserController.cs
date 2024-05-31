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

        // GET: /User/Edit/{id}
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View("Views/Admin/EditUser.cshtml", user);
        }


        // POST: /User/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IdentityUser model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                if (user == null)
                {
                    return NotFound();
                }

                user.UserName = model.UserName;
                user.Email = model.Email;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("All");
                }
                else
                {
                    // Handle update errors
                    ModelState.AddModelError("", "Failed to update user.");
                    return View(model);
                }
            }

            // If ModelState is not valid, return the view with validation errors
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                // Optionally, redirect to a different page or action after deletion
                return RedirectToAction("All");
            }
            else
            {
                // Handle delete errors
                ModelState.AddModelError("", "Failed to delete user.");
                return View("All", await _userManager.Users.ToListAsync());
            }
        }

    }
}