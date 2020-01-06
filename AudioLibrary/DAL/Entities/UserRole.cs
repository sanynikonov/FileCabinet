using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL
{
    public class UserRole : IdentityRole
    {
        public string Description { get; set; }
    }
}
