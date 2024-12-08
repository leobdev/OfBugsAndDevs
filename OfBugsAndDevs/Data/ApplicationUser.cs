using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace OfBugsAndDevs.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Required, MaxLength(25)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Avatar { get; set; } = string.Empty;

    }

}
