using ebookings.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ebookings.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
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

        // Other actions like UpdateCart, Checkout, etc., can be added as needed
    }
}
