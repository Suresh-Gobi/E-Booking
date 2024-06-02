using ebookings.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ebookings.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Books/Index
        public async Task<IActionResult> Index()
        {
            var books = await _context.Books.ToListAsync();
            return View(books);
        }

        // GET: Books/AllBooks
        public async Task<IActionResult> AllBooks()
        {
            var books = await _context.Books.ToListAsync();
            return View("Books", books);
        }




        // GET: Books/Get
        public async Task<IActionResult> Get()
        {
            var books = await _context.Books.ToListAsync();
            return View(books);
        }

        // GET: Books/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Author,Publisher,PublicationDate,ISBN,Edition,Price,Availability,CustomerReviews")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Successfully record created!";
                // return RedirectToAction(nameof(Get));
            }
            return View(book);
        }

                // GET: Books/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Author,Publisher,PublicationDate,ISBN,Edition,Price,Availability,CustomerReviews")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Book updated successfully!";
                    return RedirectToAction(nameof(Get));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(book);
        }


        // GET: Books/Delete/{id}
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
        
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Delete(int id)
{
    var book = await _context.Books.FindAsync(id);
    if (book == null)
    {
        return NotFound();
    }

    _context.Books.Remove(book);
    await _context.SaveChangesAsync();

    return Ok();
}




        private bool BookExists(int id)
        {
            return _context.Books.Any(b => b.Id == id);
        }
    }
}
