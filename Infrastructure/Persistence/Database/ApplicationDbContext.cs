using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Database;

public class ApplicationDbContext : IdentityDbContext<ApplicationIdentityUser, IdentityRole<int>, int>
{
    
    public DbSet<Category> Categories { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
}