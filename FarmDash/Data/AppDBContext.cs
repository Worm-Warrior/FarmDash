namespace FarmDash.Data;

using FarmDash.Models;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class AppDBContext : IdentityDbContext
{
    public AppDBContext(DbContextOptions<AppDBContext> options)
        : base(options) { }

    public DbSet<Farm> Farms { get; set; }
}
