using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

    }
}
