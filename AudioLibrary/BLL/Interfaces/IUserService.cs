using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IUserService : IDisposable, IUserIdService
    {
        Task AddUserAsync(UserDTO user, string password);

        Task<UserDTO> GetUserByIdAsync(string id);
        Task<UserDTO> GetUserByPhoneNumberAsync(string phoneNumber);
        Task<UserDTO> GetUserByEmailAsync(string email);
        Task<UserDTO> GetUserByUserNameAsync(string userName);
        Task<IEnumerable<UserDTO>> GetUsersBySubnameAsync(string name);
        Task<IEnumerable<UserDTO>> GetFollowersAsync(string userId);
        Task<IEnumerable<UserDTO>> GetFollowingsAsync(string userId);

        Task FollowToAccountAsync(string accountId, string followerId);

        Task<ClaimsIdentity> AuthenticateUserAsync(string userName, string password, string claimType);
        Task<bool> ExistsAsync(string email, string phoneNumber);
        Task<bool> UserNameAlreadyTakenAsync(string userName);
        Task ChangePasswordAsync(string userId, string currentPassword, string newPassword);

        Task AddToAdminsAsync(string userId);
        Task DeleteFromAdminsAsync(string userId);
        Task AddToArtistsAsync(string userId);
        Task DeleteFromArtistsAsync(string userId);
    }
}
