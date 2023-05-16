namespace FT_Api.Models
{
    public class CreateUserModel
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? RoleName { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? OrganizationName { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
