using FT_UILibrary.API;
using FT_UILibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace FT_UILibrary.Endpoint
{
    public class FTEndpoint : IFTEndpoint
    {
        private IFTAPIHelper _FTApiHelper;

        public FTEndpoint(IFTAPIHelper apiHelper)
        {
            _translationEndpoint = "leetspeak.json?text=";
            _FTApiHelper = apiHelper;
        }

        private string _translationEndpoint;
        public string EndpointUrl
        {
            get
            {
                return _translationEndpoint;
            }
        }
        public string BaseApiUrl
        {
            get
            {
                return _FTApiHelper.BaseApiUrl;
            }
        }

        public async Task<FTResponseModel> TranslateText(string text)
        {
            //TODO: Get the endpoint url from the database or from the appsettings file
            var translationEndpoint = $"{EndpointUrl}{Uri.EscapeDataString(text)}";
            using (HttpResponseMessage response = await _FTApiHelper.ApiClient.GetAsync(translationEndpoint))
            {
                if (response.IsSuccessStatusCode)
                {
                    //var content = await response.Content.ReadFromJsonAsync<TranslationResponseModel>();
                    var content = await response.Content.ReadFromJsonAsync<FunTranslationsResponse>();
                    return new FTResponseModel
                    {
                        TranslatedText = content.Contents.Translated
                    };
                }
                else
                {
                    //Handle error
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }


    //TODO: refactor and move this from here!!!
    public class FunTranslationsResponse
    {
        public FunTranslationsContents Contents { get; set; }
    }

    public class FunTranslationsContents
    {
        public string Translated { get; set; }
    }
}
