namespace FarmDash.Data;

using Microsoft.EntityFrameworkCore;
using FarmDash.Models;

public class AppDBContext : DbContext
{
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
    
    public DbSet<Farm> Farms => Set<Farm>();
}