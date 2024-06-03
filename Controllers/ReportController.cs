using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ebookings.Models;

namespace ebookings.Controllers
{
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult GenerateReport()
{
    var totalBooks = _context.Books.Count();
    var totalOrders = _context.Orders.Count();
    var totalUsers = _context.UserSignupModels.Count();

    ViewBag.TotalBooks = totalBooks;
    ViewBag.TotalOrders = totalOrders;
    ViewBag.TotalUsers = totalUsers;

    return View("~/Views/Admin/GenerateReport.cshtml");  // Update the view path
}

    }
}
