using System;
using System.ComponentModel.DataAnnotations;

namespace ebookings.Models
{
    public class Book
    {
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Author { get; set; }
        
        [Required]
        public string Publisher { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime PublicationDate { get; set; }
        
        [Required]
        public string ISBN { get; set; }
        
        public string Edition { get; set; }
        
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        
        public bool Availability { get; set; }
        
        public string CustomerReviews { get; set; }
    }
}
