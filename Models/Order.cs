using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ebookings.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public List<CartItem> Items { get; set; }
    }
}
