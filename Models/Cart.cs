namespace ebookings.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
        public int BookId { get; set; }
        public decimal Price { get; set; }
    }

    public class Cart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
