using FarmDash.Components;
using FarmDash.Data;
using FarmDash.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// ONLY use AddIdentity - remove any AddDefaultIdentity calls
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    // Password settings
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 4;
    options.Password.RequiredUniqueChars = 1;

    // User settings
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;
})
.AddEntityFrameworkStores<AppDBContext>()
.AddDefaultTokenProviders();

// Add cookie authentication configuration
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login";
    options.LogoutPath = "/logout";
    options.AccessDeniedPath = "/access-denied";
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<FarmServices>();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Your existing user creation code stays the same
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDBContext>();
    var UserManager = services.GetRequiredService<UserManager<IdentityUser>>();
    var RoleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    
    await context.Database.EnsureCreatedAsync();
    
    string[] RoleNames = { "Admin", "User" };
    foreach (var role in RoleNames)
    {
        if (!await RoleManager.RoleExistsAsync(role))
        {
            await RoleManager.CreateAsync(new IdentityRole(role));
        }
    }
    
    var TestUser = await UserManager.FindByEmailAsync("test@admin.com");
    if (TestUser == null)
    {
        var user = new IdentityUser
        {
            UserName = "test@admin.com",
            Email = "test@admin.com",
            EmailConfirmed = true
        };
        var result = await UserManager.CreateAsync(user, "Admin123!");
        if (result.Succeeded)
        {
            await UserManager.AddToRoleAsync(user, "Admin");
            Console.WriteLine("Test user created: test@admin.com / Admin123!");
        }
        else
        {
            Console.WriteLine("Failed to create test user:");
            foreach (var error in result.Errors)
            {
                Console.WriteLine($"  {error.Description}");
            }
        }
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// IMPORTANT: Authentication must come before Authorization
app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapRazorPages();
app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();