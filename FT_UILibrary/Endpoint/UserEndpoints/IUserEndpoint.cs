using FT_UILibrary.Models.UserModels;

namespace FT_UILibrary.Endpoint.UserEndpoints
{
    public interface IUserEndpoint
    {
        Task AddUserToRole(string userId, string roleName);
        Task CreateNewAccount(CreateUserModel userToCreate);
        Task<List<UserModel>?> GetAll();
        Task<Dictionary<string, string>?> GetAllRoles();
        Task RemoveUserFromRole(string userId, string roleName);
    }
}