using Microsoft.AspNetCore.Identity;

namespace backend.Database;

public class User :IdentityUser
{
    public string? Initials { get; set; }
    
}