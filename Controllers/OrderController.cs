// OrderController.cs
using ebookings.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ebookings.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public OrderController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Order/Checkout
        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }

        // POST: Order/CompleteOrder
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompleteOrder(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                var cartItems = await _context.CartItems.Include(c => c.Book).Where(c => c.UserId == userId).ToListAsync();

                if (!cartItems.Any())
                {
                    ModelState.AddModelError("", "Your cart is empty.");
                    return View("Checkout", model);
                }

                var order = new Order
                {
                    FullName = model.FullName,
                    Address = model.Address,
                    PostalCode = model.PostalCode,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    UserId = userId,
                    Items = cartItems
                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                // Clear the cart after successful order
                _context.CartItems.RemoveRange(cartItems);
                await _context.SaveChangesAsync();

                return RedirectToAction("OrderConfirmation");
            }

            return View("Checkout", model);
        }

        // GET: Order/OrderConfirmation
        public IActionResult OrderConfirmation()
        {
            return View();
        }
    }
}
