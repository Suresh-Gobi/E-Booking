using Microsoft.AspNetCore.Mvc;

namespace ebookings.Controllers
{
    [Route("Admin/Adminboard")]
    public class AdminboardController : Controller
    {
        [Route("")]
        public IActionResult AdminDashboard()
        {
            return View("~/Views/Admin/Adminboard.cshtml");
        }
    }
}
