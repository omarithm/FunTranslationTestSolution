using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FT_UILibrary.Models.UserModels
{
    public class CreateUserModel
    {
        public string Username { get; set; }

        [EmailAddress(ErrorMessage = "Not valid email address")]
        [Required(ErrorMessage = "Required")]
        public string Email { get; set; }

        public string Password { get; set; }
        public string RoleName { get; set; }

        public string? OrganizationName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
