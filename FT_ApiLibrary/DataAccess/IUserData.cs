using FT_ApiLibrary.Models;

namespace FT_ApiLibrary.DataAccess
{
    public interface IUserData
    {
        UserModel GetUserById(string Id);
        void SaveUser(UserModel user);
    }
}