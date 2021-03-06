﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API
{
    public class UserModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string PhotoPath { get; set; }
    }
}