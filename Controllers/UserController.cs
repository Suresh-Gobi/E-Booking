using Microsoft.AspNetCore.Mvc;

namespace YourNamespace.Controllers
{
    public class UserController : Controller
    {
        // GET: /User/Dashboard
        public IActionResult Dashboard()
        {
            return View("Dashboard"); // Renders the Dashboard.cshtml view
        }
    }
}
