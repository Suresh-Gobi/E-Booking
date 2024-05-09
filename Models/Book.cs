using System.ComponentModel.DataAnnotations;

namespace bookstore.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Author { get; set; }

        public int YearPublished { get; set; }

        // Add more properties as needed
    }
}
