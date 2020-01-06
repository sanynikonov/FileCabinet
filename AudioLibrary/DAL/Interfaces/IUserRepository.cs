using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user, string password);

        Task UpdateUserAsync(User user);
        Task ChangePasswordAsync(string userId, string currentPasword, string newPassword);

        Task AddToRoleAsync(string userId, string role);
        Task RemoveFromRoleAsync(string userId, string role);

        Task<User> GetUserByIdAsync(string id);
        Task<User> GetUserByPhoneNumberAsync(string phoneNumber);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByUserNameAsync(string userName);

        Task<IEnumerable<User>> GetUsersBySubnameAsync(string name);
        Task<IEnumerable<User>> GetAllUsersAsync();

        Task<IEnumerable<UserRole>> GetUserRolesAsync();
        Task<IEnumerable<Country>> GetCountriesAsync();
        Task<ClaimsIdentity> AuthenticateUserAsync(string userName, string password, string claimType);
    }
}
