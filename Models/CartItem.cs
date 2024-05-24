using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ebookings.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        // Link to the user
        [Required]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}