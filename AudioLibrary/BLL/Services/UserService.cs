using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DAL;

namespace BLL
{
    public class UserService : Service, IUserService
    {
        public UserService(IUnitOfWork unit, IMapper mapper) : base(unit, mapper)
        {
        }

        public async Task AddUserAsync(UserDTO item, string password)
        {
            var user = mapper.Map<User>(item);
            var countries = await unit.UserRepository.GetCountriesAsync();
            user.Country = countries.FirstOrDefault(x => x.Name == item.Country);
            if (user.Country == null)
                throw new NotFoundException();
            await unit.UserRepository.AddUserAsync(user, password);
        }

        public async Task<ClaimsIdentity> AuthenticateUserAsync(string userName, string password, string claimType)
        {
            return await unit.UserRepository.AuthenticateUserAsync(userName, password, claimType);
        }

        public async Task ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            await unit.UserRepository.ChangePasswordAsync(userId, currentPassword, newPassword);
        }

        public async Task FollowToAccountAsync(string accountId, string followerId)
        {
            var userToFollow = await unit.UserRepository.GetUserByIdAsync(accountId);
            var userThatFollows = await unit.UserRepository.GetUserByIdAsync(followerId);
            userToFollow.Followers.Add(userThatFollows);
            await unit.UserRepository.UpdateUserAsync(userToFollow);
        }

        public async Task<UserDTO> GetUserByIdAsync(string id)
        {
            var user = await unit.UserRepository.GetUserByIdAsync(id);
            var userDTO = mapper.Map<UserDTO>(user);
            return userDTO;
        }


        public async Task<IEnumerable<UserDTO>> GetFollowersAsync(string userId)
        {
            var user = await unit.UserRepository.GetUserByIdAsync(userId);
            return user.Followers.Select(x => mapper.Map<UserDTO>(x));
        }

        public async Task<IEnumerable<UserDTO>> GetFollowingsAsync(string userId)
        {
            var user = await unit.UserRepository.GetUserByIdAsync(userId);
            return user.Followings.Select(x => mapper.Map<UserDTO>(x));
        }

        public async Task<UserDTO> GetUserByEmailAsync(string email)
        {
            var user = await unit.UserRepository.GetUserByEmailAsync(email);
            return mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> GetUserByPhoneNumberAsync(string phoneNumber)
        {
            var user = await unit.UserRepository.GetUserByPhoneNumberAsync(phoneNumber);
            return mapper.Map<UserDTO>(user);
        }

        public async Task<IEnumerable<UserDTO>> GetUsersBySubnameAsync(string subname)
        {
            var users = await unit.UserRepository.GetUsersBySubnameAsync(subname);
            return users.Select(x => mapper.Map<UserDTO>(x));
        }

        public async Task<bool> UserNameAlreadyTakenAsync(string userName)
        {
            var user = await unit.UserRepository.GetUserByUserNameAsync(userName);
            return user != null;
        }

        public async Task<bool> ExistsAsync(string email, string phoneNumber)
        {
            var userByEmail = await unit.UserRepository.GetUserByEmailAsync(email);
            var userByPhone = await unit.UserRepository.GetUserByPhoneNumberAsync(phoneNumber);
            return userByEmail != null || userByPhone != null;
        }

        public async Task AddToAdminsAsync(string userId)
        {
            await unit.UserRepository.AddToRoleAsync(userId, "Admin");
        }

        public async Task AddToArtistsAsync(string userId)
        {
            await unit.UserRepository.AddToRoleAsync(userId, "Artist");
        }

        public async Task DeleteFromAdminsAsync(string userId)
        {
            await unit.UserRepository.RemoveFromRoleAsync(userId, "Admin");
        }

        public async Task DeleteFromArtistsAsync(string userId)
        {
            await unit.UserRepository.RemoveFromRoleAsync(userId, "Artist");
        }
    }
}
