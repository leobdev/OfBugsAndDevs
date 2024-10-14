﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OfBugsAndDevs.Data;
using OfBugsAndDevs.Data.Entities;

namespace OfBugsAndDevs.Services
{

	internal static class AdminAccount
	{
		public const string Name = "Leo Barnuevo";
		public const string Email = "leocoding.net@gmail.com";
		public const string Role = "Admin";
		public const string Password = "Abc!123@";
	}

	public interface ISeedService
	{
		Task SeedDataAsync();
	}

	public class SeedService : ISeedService
	{

		private readonly ApplicationDbContext _context;
		private readonly IUserStore<ApplicationUser> _userStore;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public SeedService(ApplicationDbContext context,
			IUserStore<ApplicationUser> userStore,
			UserManager<ApplicationUser> userManager,
			RoleManager<IdentityRole> roleManager)
		{
			_context = context;
			_userStore = userStore;
			_userManager = userManager;
			_roleManager = roleManager;
		}

#if DEBUG
		private async Task MigrateDatabaseAsync()
		{
			if ((await _context.Database.GetPendingMigrationsAsync()).Any())
			{
				await _context.Database.MigrateAsync();
			}
		}
#endif

		public async Task SeedDataAsync()
		{
			await MigrateDatabaseAsync();

			//Seed AdminRole
			if (await _roleManager.FindByNameAsync(AdminAccount.Role) is null)
			{
				var adminRole = new IdentityRole(AdminAccount.Role);
				var result = await _roleManager.CreateAsync(adminRole);
				if (!result.Succeeded)
				{
					var errorString = result.Errors.Select(e => e.Description);
					throw new Exception($"Error in creating Admin Role {Environment.NewLine}: {string.Join(Environment.NewLine, errorString)}");
				}
			}

			//Seed Admin User
			var adminUser = await _userManager.FindByEmailAsync(AdminAccount.Email);
			if (adminUser is null)
			{
				adminUser = new ApplicationUser();
				adminUser.Name = AdminAccount.Name;
				await _userStore.SetUserNameAsync(adminUser, AdminAccount.Email, CancellationToken.None);

				var emailStore = (IUserEmailStore<ApplicationUser>)_userStore;
				await emailStore.SetEmailAsync(adminUser, AdminAccount.Email, CancellationToken.None);
				var result = await _userManager.CreateAsync(adminUser, AdminAccount.Password);
				if (!result.Succeeded)
				{
					var errorString = result.Errors.Select(e => e.Description);
					throw new Exception($"Error in creating Admin User {Environment.NewLine}: {string.Join(Environment.NewLine, errorString)}");
				}
			}


			//Seed Categories
			if (!await _context.Categories.AsNoTracking().AnyAsync())
			{
				//There are no categories, then create 

				await _context.Categories.AddRangeAsync(Category.GetCategories());
				await _context.SaveChangesAsync();
			}

		}

	}
}
