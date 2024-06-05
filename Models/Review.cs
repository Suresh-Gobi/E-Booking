namespace ebookings.Models
{
    public class Review
    {
        public int Id { get; set; } // Primary key
        public int Rating { get; set; }
        public string ReviewText { get; set; }

        // Other properties as needed
    }
}
