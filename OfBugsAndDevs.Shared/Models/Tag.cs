using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfBugsAndDevs.Shared.Models
{
    public class Tag
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public string? AuthorId { get; set; }

        [StringLength(25, ErrorMessage = "The {0} must be at least {2} and no more than {1}",  MinimumLength = 2)]
        public required string Text { get; set; }


        //Navigation properties
        public virtual Post? Post { get; set; }

        public virtual IdentityUser? Author { get; set; }
    }
}
