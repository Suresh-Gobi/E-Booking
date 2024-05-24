using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ebookings.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public List<CartItem> Items { get; set; } = new List<CartItem>();

        // Link to the user
        [Required]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}