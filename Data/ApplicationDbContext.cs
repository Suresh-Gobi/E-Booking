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

    // Other DbSet properties as needed
}
