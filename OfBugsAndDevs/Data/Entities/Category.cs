using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OfBugsAndDevs.Data.Entities
{
	public class Category
	{
		
		public int CategoryID { get; set; }
		[Required, MaxLength(50)]
		[Display(Name = "Category")]
		public string CategoryName { get; set; } 

		[MaxLength(100)]
		public string Slug { get; set; } 

		public bool ShowOnNavBar { get; set; }

		public Category Clone() => (Category)MemberwiseClone()!;


		public static Category[] GetCategories()
		{
			return
				[
				new Category { CategoryName = "C#", Slug = "c-sharp", ShowOnNavBar = true },
				new Category { CategoryName = "ASP.NET Core", Slug = "aspnet-core", ShowOnNavBar = true },
				new Category { CategoryName = "Blazor", Slug = "blazor", ShowOnNavBar = true },
				new Category { CategoryName = "SQL Server", Slug = "sql-server", ShowOnNavBar = true },
				new Category { CategoryName = "Entity Framework", Slug = "entity-framework", ShowOnNavBar = true },
				new Category { CategoryName = "Data Structures and Algorithms", Slug = "datastructures-and-algorithms", ShowOnNavBar = true },
				new Category { CategoryName = "Developer Life", Slug = "dev-life", ShowOnNavBar = true }

				];
		}

	}
}
