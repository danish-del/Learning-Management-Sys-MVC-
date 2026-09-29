using Microsoft.AspNetCore.Identity;

namespace LMS.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}
