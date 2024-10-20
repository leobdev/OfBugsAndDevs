using System.ComponentModel.DataAnnotations;

namespace OfBugsAndDevs.Data.Entities
{
	public class BlogPost
	{
		public int BlogPostID { get; set; }

		[Required, MaxLength(100)]
		public string Title { get; set; }

		[MaxLength(150)]
		public string Slug { get; set; }

		[MaxLength(100)]
		public string Image { get; set; }

        [Required, MaxLength(500)]
		public string Introduction { get; set; }

		public string Content { get; set; }

		[Range(1, int.MaxValue, ErrorMessage = "Please select a valid category")]
		public int CategoryID { get; set; }

		public string UserID { get; set; }

		public bool IsPublished { get; set; }

		public int ViewCount { get; set; }

		public bool IsFeatured { get; set; }

		public DateTime Created { get; set; }

		public DateTime? Published { get; set; }

		public virtual Category Category { get; set; }

		public virtual ApplicationUser User { get; set; }


	}
}
