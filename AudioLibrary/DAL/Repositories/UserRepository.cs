using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserRepository : IUserRepository
    {
        private UserManager userManager;
        private RoleManager roleManager;
        private AudioLibraryContext context;

        public UserRepository(AudioLibraryContext context)
        {
            userManager = new UserManager(context);
            roleManager = new RoleManager(context);
            this.context = context;
        }

        public async Task AddUserAsync(User user, string password)
        {
            await userManager.CreateAsync(user, password);
            await userManager.AddToRoleAsync(user.Id, "User");
        }

        public async Task UpdateUserAsync(User user)
        {
            await userManager.UpdateAsync(user);
        }

        public async Task AddToRoleAsync(string userId, string role)
        {
            await userManager.AddToRoleAsync(userId, role);
        }

        public async Task RemoveFromRoleAsync(string userId, string role)
        {
            await userManager.RemoveFromRoleAsync(userId, role);
        }

        public async Task<ClaimsIdentity> AuthenticateUserAsync(string userName, string password, string claimType)
        {
            var user = await userManager.FindAsync(userName, password);
            return await userManager.CreateIdentityAsync(user, claimType);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await userManager.Users.ToListAsync();
        }

        public async Task<IEnumerable<Country>> GetCountriesAsync()
        {
            return await context.Countries.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            return await userManager.FindByIdAsync(id);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        public async Task<User> GetUserByPhoneNumberAsync(string phoneNumber)
        {
            return await userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
        }

        public async Task<IEnumerable<UserRole>> GetUserRolesAsync()
        {
            return await roleManager.Roles.ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUsersBySubnameAsync(string name)
        {
            return await userManager.Users.Where(
                x => (x.UserName + x.FirstName + x.LastName).ToLower().Contains(name.ToLower()))
                .ToListAsync();
        }

        public async Task ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            await userManager.ChangePasswordAsync(userId, currentPassword, newPassword);
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            return await userManager.FindByNameAsync(userName);
        }
    }
}
