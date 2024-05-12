using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace bookstore.Models
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        
        [Required]
        public string? Username { get; set; }

        [Required]
        [EmailAddress]
       public string? Email { get; set; } 

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        public string? FullName { get; set; }

        public bool IsActive { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>(); // Initialize the collection

        // Add any additional properties or methods here
    }
}
