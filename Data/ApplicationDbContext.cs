using Microsoft.EntityFrameworkCore;
using bookstore.Models; // Add this line to import the namespace for Book

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // DbSet properties for your models
    public DbSet<Book> Books { get; set; }
    public DbSet<ApplicationUser> Users { get; set; }

    // Other DbSet properties as needed
}
