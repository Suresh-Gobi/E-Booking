using System.Collections.Generic;

namespace ebookings.Models
{
    public class UserDashboardViewModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public List<Book> Books { get; set; } // Add list of books
    }
}
