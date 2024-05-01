using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfBugsAndDevs.Shared.Models
{
    public class BlogUser : IdentityUser
    {
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and no more than {1} characters long", MinimumLength = 2)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and no more than {1} characters long", MinimumLength = 2)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [NotMapped]
        [Display(Name = "First Name")]
        public string FullName { get { return  $"{FirstName} {LastName}";  } }

        public byte[]? ImageData { get; set; } 

        public string ContentType { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and no more than {1} characters long", MinimumLength = 2)]
        public string FacebookUrl { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and no more than {1} characters long", MinimumLength = 2)]
        public string TwitterUrl { get; set; } = string.Empty;

        //Navigation properties
        public virtual ICollection<Blog> Blogs { get; set; } = new HashSet<Blog>();

        public virtual ICollection<Post> Posts { get; set; } = new HashSet<Post>();

    }
}
