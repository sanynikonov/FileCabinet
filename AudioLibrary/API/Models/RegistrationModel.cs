using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API
{
    public class RegistrationModel
    {
        [Required(AllowEmptyStrings = false)]
        public string UserName { get; set; }

        [Required]
        [MinLength(7, ErrorMessage = "Password should contain at least 7 characters")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string PasswordConfirm { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Country { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string PhoneNumber { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string PhotoPath { get; set; }
    }
}