using Microsoft.AspNetCore.Identity;

namespace MVC_Mini_Project.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
