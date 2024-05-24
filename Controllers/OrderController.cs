using ebookings.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ebookings.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
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
                var order = new Order
                {
                    FullName = model.FullName,
                    Address = model.Address,
                    PostalCode = model.PostalCode,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Items = await _context.CartItems.Include(c => c.Book).ToListAsync() // Assuming all cart items are part of the order
                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                // Clear the cart after successful order
                _context.CartItems.RemoveRange(order.Items);
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
