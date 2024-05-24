using ebookings.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ebookings.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CartController> _logger;

        public CartController(ApplicationDbContext context, ILogger<CartController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Cart/Index
        public async Task<IActionResult> Index()
        {
            var cartItems = await _context.CartItems.Include(c => c.Book).ToListAsync();
            return View(cartItems);
        }

        // POST: Cart/AddToCart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItems.FirstOrDefaultAsync(ci => ci.BookId == id);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    BookId = id,
                    Quantity = 1,
                    Price = book.Price,
                    Book = book
                };
                _context.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity++;
                _context.CartItems.Update(cartItem);
            }

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Book added to cart successfully!";
            return RedirectToAction(nameof(Index));
        }

        // POST: Cart/RemoveFromCart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem == null)
            {
                return NotFound();
            }

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Book removed from cart successfully!";
            return RedirectToAction(nameof(Index));
        }

        // GET: Cart/Order
        public IActionResult Order()
        {
            return View(new Order());
        }

        // POST: Cart/CompleteOrder
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompleteOrder(Order order)
        {
            if (ModelState.IsValid)
            {
                using var transaction = await _context.Database.BeginTransactionAsync();

                try
                {
                    order.OrderDate = DateTime.Now;
                    _context.Orders.Add(order);
                    await _context.SaveChangesAsync();

                    var cartItems = await _context.CartItems.Include(c => c.Book).ToListAsync();
                    foreach (var item in cartItems)
                    {
                        var orderDetail = new OrderDetail
                        {
                            OrderId = order.Id,
                            BookId = item.BookId,
                            Quantity = item.Quantity,
                            Price = item.Price
                        };
                        _context.OrderDetails.Add(orderDetail);
                        _context.CartItems.Remove(item); // Clear the cart
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    
                    TempData["SuccessMessage"] = "Order placed successfully!";
                    return RedirectToAction("Index", "Books"); // Redirect to a suitable page
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "Error completing order.");
                    ModelState.AddModelError("", "There was an error processing your order. Please try again.");
                }
            }
            return View("Order", order);
        }
    }
}
