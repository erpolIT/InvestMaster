using backend.Models;
using Microsoft.AspNetCore.Identity;

namespace backend.Database;

public class User : IdentityUser
{
    public string? Initials { get; set; }
    
    public ICollection<Portfolio> Portfolios { get; set; }
}