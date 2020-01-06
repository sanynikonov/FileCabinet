using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserManager : UserManager<User>
    {
        public UserManager(DbContext context) : base(new UserStore<User>(context))
        {
        }
    }
}
