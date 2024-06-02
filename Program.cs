using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Retrieve connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add DbContext with SQL Server provider
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add Identity services
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>(); // Configure Identity to use your ApplicationDbContext

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // Handle errors with a custom error page
    app.UseExceptionHandler("/Home/Error");
    // Enable HTTP Strict Transport Security (HSTS) for enhanced security
    app.UseHsts();
}

// Redirect HTTP requests to HTTPS
app.UseHttpsRedirection();
// Serve static files like CSS, JavaScript, and images
app.UseStaticFiles();

// Enable routing for MVC
app.UseRouting();

// Enable authentication middleware
app.UseAuthentication();
// Enable authorization middleware
app.UseAuthorization();

// Map default controller route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Map account controller route for signup action
app.MapControllerRoute(
    name: "account",
    pattern: "Account/{action=Signup}/{id?}",
    defaults: new { controller = "Account", action = "Signup" });

app.MapControllerRoute(
    name: "login",
    pattern: "Account/{action=Login}/{id?}",
    defaults: new { controller = "Account", action = "Login" });

app.MapControllerRoute(
    name: "admin",
    pattern: "Admin/{action=AdminSignup}/{id?}",
    defaults: new { controller = "Admin", action = "AdminSignup" });

app.MapControllerRoute(
    name: "admin",
    pattern: "Admin/{action=AdminLogin}/{id?}",
    defaults: new { controller = "Admin", action = "AdminLogin" });

app.MapControllerRoute(
    name: "createBook",
    pattern: "Books/Create",
    defaults: new { controller = "Books", action = "Create" });

app.MapControllerRoute(
    name: "getBooks",
    pattern: "Books/Get",
    defaults: new { controller = "Books", action = "Get" });

app.MapControllerRoute(
    name: "editBook",
    pattern: "Books/Edit/{id}", // Specify the route pattern with a placeholder for ID
    defaults: new { controller = "Books", action = "Edit" });

app.MapControllerRoute(
    name: "delete_book",
    pattern: "Books/Delete/{id}",
    defaults: new { controller = "Books", action = "Delete" });

app.MapControllerRoute(
    name: "user-dashboard",
    pattern: "User/Dashboard",
    defaults: new { controller = "User", action = "Dashboard" }
);

app.MapControllerRoute(
    name: "cart",
    pattern: "Cart/Index",
    defaults: new { controller = "Cart", action = "Index" }
);

app.MapControllerRoute(
    name: "all-users",
    pattern: "User/All",
    defaults: new { controller = "User", action = "All" }
);

app.MapControllerRoute(
    name: "edit-user",
    pattern: "User/Edit/{id}",
    defaults: new { controller = "User", action = "Edit" }
);

app.MapControllerRoute(
    name: "all-orders",
    pattern: "Order/All",
    defaults: new { controller = "Order", action = "AllDetails" }
);

app.MapControllerRoute(
    name: "allBooks",
    pattern: "Books/AllBooks",
    defaults: new { controller = "Books", action = "AllBooks" }
);

// Start the application
app.Run();
