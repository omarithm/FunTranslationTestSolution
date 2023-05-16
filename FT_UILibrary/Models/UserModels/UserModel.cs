using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FT_UILibrary.Models.UserModels
{
    public class UserModel
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        public Dictionary<string, string> Roles { get; set; } = new Dictionary<string, string>();

        public string RoleToDisplay
        {
            get
            {
                return string.Join(", ", Roles.Select(x => x.Value));
            }
        }
    }
}
