namespace ebookings.Models
{
    public class ReviewViewModel
    {
        public string Review { get; set; }
        public int Rating { get; set; }
        public string ReviewText { get; internal set; }
    }
}
