using System.ComponentModel.DataAnnotations;

namespace OfBugsAndDevs.Data.Entities
{
	public class Category
	{
		public int CategoryID { get; set; }
		[Required, MaxLength(50)]
		public string CategoryName { get; set; }

		[Required, MaxLength(100)]
		public string Slug { get; set; }

		public bool ShowOnNavBar { get; set; }


	}
}
