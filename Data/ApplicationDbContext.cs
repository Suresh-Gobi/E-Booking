using ebookings.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<UserSignupModel> UserSignupModels { get; set; }
    public DbSet<AdminSignupModel> AdminSignupModel { get; set; }
    public DbSet<Book> Books { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        // public DbSet<ReviewViewModel> Reviews { get; set; }
        public DbSet<Review> Reviews { get; set; }

    public object Items { get; internal set; }

    // Other DbSet properties as needed
}
