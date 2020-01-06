using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL
{
    public class RoleManager : RoleManager<UserRole>
    {
        public RoleManager(DbContext context) : base(new RoleStore<UserRole>(context))
        {
            
        }
    }
}
