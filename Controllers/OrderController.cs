// Controllers/OrderController.cs
using ebookings.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ebookings.Controllers
{
    // [Authorize]
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
        // Controllers/OrderController.cs
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> CompleteOrder(OrderViewModel model)
{
    if (ModelState.IsValid)
    {
        var userId = _userManager.GetUserId(User);
        var cartItems = await _context.CartItems
            .Include(c => c.Book)
            .Where(c => c.UserId == userId)
            .ToListAsync();

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
            Items = new List<CartItem>(), // Initialize the Items list
            Status = OrderStatus.Delivered
        };

        foreach (var item in cartItems)
        {
            order.Items.Add(new CartItem
            {
                BookId = item.BookId,
                Quantity = item.Quantity,
                Price = item.Price,
                UserId = userId, // Set the UserId for each CartItem
                Review = item.Review // Optional, if you want to include review from cart items
            });
        }

        _context.Orders.Add(order);

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

        // GET: Order/MyOrders
        public async Task<IActionResult> MyOrders()
        {
            var userId = _userManager.GetUserId(User);
            var orders = await _context.Orders
                                        .Include(o => o.Items)
                                        .ThenInclude(i => i.Book)
                                        .Where(o => o.UserId == userId)
                                        .ToListAsync();
            return View(orders);
        }

//         [HttpGet]
// public async Task<IActionResult> Review(int id)
// {
//     var cartItem = await _context.CartItems.Include(c => c.Book).FirstOrDefaultAsync(c => c.Id == id);
//     if (cartItem == null)
//     {
//         return NotFound();
//     }

//     var model = new ReviewViewModel
//     {
//         CartItemId = cartItem.Id,
//         Review = cartItem.Review
//     };

//     return View(model);
// }

// // POST: Order/SubmitReview
// [HttpPost]
// [ValidateAntiForgeryToken]
// public async Task<IActionResult> SubmitReview(ReviewViewModel model)
// {
//     if (ModelState.IsValid)
//     {
//         var cartItem = await _context.CartItems.Include(c => c.Book).FirstOrDefaultAsync(c => c.Id == model.CartItemId);
//         if (cartItem == null)
//         {
//             return NotFound();
//         }

//         cartItem.Review = model.Review;
//         _context.Update(cartItem);
//         await _context.SaveChangesAsync();

//         return RedirectToAction("MyOrders");
//     }

//     return View("Review", model);
// }

        // GET: Order/AllDetails
        public async Task<IActionResult> AllDetails()
        {
            var orders = await _context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Book)
                .ToListAsync();
            
            return View(orders);
        }

       // POST: Order/UpdateStatus
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            // Update status using the new method in the Order model
            order.UpdateStatus(status);

            _context.Update(order);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(AllDetails));
        }


[HttpGet]
public IActionResult Review()
{
    // Logic to initialize model or return an empty view
    return View();
}

// POST: Order/Delete/5
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Delete(int id)
{
    var order = await _context.Orders.FindAsync(id);
    if (order == null)
    {
        return NotFound();
    }

    _context.Orders.Remove(order);
    await _context.SaveChangesAsync();
    return RedirectToAction(nameof(MyOrders));
}



[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> SubmitReview(Review model)
{
    if (ModelState.IsValid)
    {
        // Create a new Review instance with the submitted data
        var review = new Review
        {
            Rating = model.Rating,
            ReviewText = model.ReviewText
        };

        // Add the review to the context and save changes
        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();

        // Redirect to the MyOrders action after successful review submission
        return RedirectToAction(nameof(MyOrders));
    }

    // If the model state is not valid, return the Review view with validation errors
    return View("Review", model);
}


        public async Task<IActionResult> AllReviews()
        {
            var reviews = await _context.Reviews.ToListAsync();

            var viewModel = new ReviewListViewModel // Assuming ReviewListViewModel is a model to hold a list of reviews
            {
                Reviews = reviews
            };

            return View(viewModel); // Assuming your view is strongly typed to ReviewListViewModel
        }

    }
}
