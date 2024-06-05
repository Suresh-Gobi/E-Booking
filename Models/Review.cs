namespace ebookings.Models
{
    public class Review
    {
        public int ReviewId { get; set; } // Primary key
        public int Rating { get; set; }
        public string ReviewText { get; set; }
        public List<Review> Reviews { get; internal set; }

        // Other properties as needed
    }
}
