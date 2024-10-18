using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Ogani.DAL.DataContext.Entities;
using Ogani.DAL.Enums;

namespace Ogani.DAL.DataContext
{
    public class DataInitalizer
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly AppUser _admin;
        private readonly string _adminPassword;
        public DataInitalizer(AppDbContext appDbContext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _admin = _configuration.GetSection("AdminOptions").Get<AppUser>() ?? new();
            _adminPassword = _configuration.GetSection("AdminOptions")["Password"] ?? "";
        }

        public async Task SeedDataAsync()
        {
            await _appDbContext.Database.MigrateAsync();

            await _addRolesAsync();

            await _addAdminAsync();
        }

        private async Task _addRolesAsync()
        {
            foreach (var r in Enum.GetNames(typeof(IdentityRoles)))
            {
                if (await _roleManager.Roles.AnyAsync(x => x.Name == r.ToString()))
                    continue;

                IdentityRole role = new() { Name = r };

                await _roleManager.CreateAsync(role);
            }
        }
        private async Task _addAdminAsync()
        {
            var existUser = await _userManager.FindByNameAsync(_admin.UserName ?? "");

            if (existUser is not null)
                return;

            await _userManager.CreateAsync(_admin, _adminPassword);
            await _userManager.AddToRoleAsync(_admin, IdentityRoles.Admin.ToString());
        }
    }
}
