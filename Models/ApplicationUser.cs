using Microsoft.AspNetCore.Identity;

namespace Round_OP.Models
{
    public class ApplicationUser : IdentityUser
    {
         public string? FullName { get; set; }
    }
}
