using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bookstore.Models;

namespace bookstore.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Signup(ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                // Add the user to the Users DbSet
                _context.Users.Add(user);

                // Save changes to the database
                await _context.SaveChangesAsync();

                // Redirect to home page after successful signup
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Log ModelState errors for debugging
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                // If ModelState is not valid, return the signup view with validation errors
                return View(user);
            }
        }

         [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return detailed bad request response with validation errors
            }

            // Check if the username and password match in the database
            var authenticatedUser = await _context.Users.FirstOrDefaultAsync(u =>
                u.Username == model.Username && u.Password == model.Password);

            if (authenticatedUser != null)
            {
                return Ok("Login successful"); // Return an OK response with login success message
            }
            else
            {
                return NotFound("Invalid username or password"); // Return a not found response for failed login
            }
        }







    }
}
