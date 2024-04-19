using Microsoft.AspNetCore.Identity;

namespace Fantasia.DataAccess.Entity.Account;
public class AppUser : IdentityUser
{
    public string? Name { get; set; }
}