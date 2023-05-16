using FT_UILibrary.Models.UserModels;

namespace FT_UILibrary.API
{
    public interface IAPIHelper
    {
        HttpClient ApiClient { get; }

        Task GetLoggedInUserInfo(string token);
        Task<TokenModel> GetToken(string username, string password);
        void LogOffUser();
    }
}