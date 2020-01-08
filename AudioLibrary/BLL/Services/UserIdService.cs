using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DAL;

namespace BLL
{
    public class UserIdService : Service, IUserIdService
    {
        public UserIdService(IUnitOfWork unit, IMapper mapper) : base(unit, mapper)
        {
        }

        public async Task<string> GetUserIdByUserName(string userName)
        {
            var user = await unit.UserRepository.GetUserByUserNameAsync(userName);
            return user.Id;
        }
    }
}
