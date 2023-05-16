using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using FT_UILibrary.Models.UserModels;
using Microsoft.Extensions.Configuration;

namespace FT_UILibrary.API
{
    public class APIHelper : IAPIHelper
    {
        private ILoggedInUserModel _loggedInUser;
        private IConfiguration _config;
        public APIHelper(IConfiguration config
                        ,ILoggedInUserModel loggedInUser)
        {
            _config = config;
            _loggedInUser = loggedInUser;
            InitializeClient();
        }

        private void InitializeClient()
        {
            //TODO: fix this on android, the ConfigurationManager.AppSettings["apiURL"] does not work on android
            string? apiURL = _config.GetSection("ApiURLs:ApiURL").Value;

            //#if DEBUG
            //apiURL = ConfigurationManager.AppSettings["apiURL_Local"];
            //#endif

            _apiClient = new HttpClient();
            _apiClient.BaseAddress = new Uri(apiURL);
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }


        //We created the following property so that we can get access to the
        //_apiClient from other classes
        private HttpClient _apiClient;
        public HttpClient ApiClient
        {
            get
            {
                return _apiClient;
            }
        }

        //The following is to check username and password to login
        public async Task<TokenModel> GetToken(string username, string password)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("grant_type", "password")
            });

            using (HttpResponseMessage response = await _apiClient.PostAsync("/Token", data))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<TokenModel>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }


        public async Task GetLoggedInUserInfo(string token)
        {
            _apiClient.DefaultRequestHeaders.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _apiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");


            using (HttpResponseMessage response = await _apiClient.GetAsync("/api/User"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<LoggedInUserModel>();

                    _loggedInUser.Token = token;
                    _loggedInUser.Id = result.Id;
                    _loggedInUser.CreatedDate = result.CreatedDate;
                    _loggedInUser.EmailAddress = result.EmailAddress;
                    _loggedInUser.FirstName = result.FirstName;
                    _loggedInUser.LastName = result.LastName;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public void LogOffUser()
        {
            _apiClient.DefaultRequestHeaders.Clear();
        }
    }
}
