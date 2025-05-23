using Microsoft.AspNetCore.Identity;

namespace Strategy.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}