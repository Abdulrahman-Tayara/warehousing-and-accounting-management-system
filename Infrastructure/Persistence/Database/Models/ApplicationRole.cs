using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Persistence.Database.Models;

public class ApplicationRole : IdentityRole<int>
{
    public string Permissions { get; set; }
}