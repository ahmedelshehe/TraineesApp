using Microsoft.AspNetCore.Identity;
using TraineesApp.Models;

namespace TraineesApp.Services
{
    public class UserService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task AddUserAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new AppUser { Email = email, PasswordHash = password };

                await _userManager.CreateAsync(user);
            }
        }
    }
}
