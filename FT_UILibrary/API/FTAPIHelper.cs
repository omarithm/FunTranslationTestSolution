using FT_UILibrary.Models;
using FT_UILibrary.Models.UserModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace FT_UILibrary.API
{
    public class FTAPIHelper : IFTAPIHelper
    {
        private HttpClient _apiClient;
        private IConfiguration _config;

        public FTAPIHelper(IConfiguration config)
        {
            _config = config;
            InitializeClient();
        }

        private void InitializeClient()
        {
            //TODO: fix this on android, the ConfigurationManager.AppSettings["apiURL"] does not work on android
            //string? apiURL = ConfigurationManager.AppSettings["FunTranslationsApiURL"];
            _baseApiUrl = _config.GetSection("ApiURLs:FunTranslationsApiURL").Value;

            _apiClient = new HttpClient();
            _apiClient.BaseAddress = new Uri(BaseApiUrl);

        }


        private string _baseApiUrl;

        public string BaseApiUrl
        {
            get { return _baseApiUrl; }
        }


        public HttpClient ApiClient
        {
            get
            {
                return _apiClient;
            }
        }
    }
}
