using FT_UILibrary.API;
using FT_UILibrary.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace FT_UILibrary.Endpoint.UserEndpoints
{
    public class UserEndpoint : IUserEndpoint
    {
        private readonly IAPIHelper _apiHelper;
        public UserEndpoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }



        public async Task<List<UserModel>?> GetAll()
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("api/User/Admin/GetAllUsers"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<List<UserModel>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<Dictionary<string, string>?> GetAllRoles()
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("api/User/Admin/GetAllRoles"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task AddUserToRole(string userId, string roleName)
        {
            var data = new { userId, roleName };

            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/User/Admin/AddRole", data))
            {
                if (response.IsSuccessStatusCode == false)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task RemoveUserFromRole(string userId, string roleName)
        {
            var data = new { userId, roleName };

            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/User/Admin/RemoveRole", data))
            {
                if (response.IsSuccessStatusCode == false)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task CreateNewAccount(CreateUserModel userToCreate)
        {
            var data = new { userToCreate.Username, userToCreate.Password, userToCreate.Email, userToCreate.RoleName };

            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/User/Register", data))
            {
                if (response.IsSuccessStatusCode == false)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
