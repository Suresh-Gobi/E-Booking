using Microsoft.AspNetCore.Identity;

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
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public List<CartItem> Items { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.PendingDelivery;
    }

    public enum OrderStatus
    {
        PendingDelivery,
        Delivered
    }
}