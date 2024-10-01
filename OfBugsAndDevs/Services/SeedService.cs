using OfBugsAndDevs.Data;

namespace OfBugsAndDevs.Services
{
	public class SeedService
	{
		private readonly ApplicationDbContext _context;
		public SeedService(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task SeedDataAsync()
		{

			//Seed AdminRole

			//Seed Admin User

			//Seed Categories

		}

	}
}
