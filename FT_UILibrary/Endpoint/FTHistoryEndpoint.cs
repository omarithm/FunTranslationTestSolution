using FT_UILibrary.API;
using FT_UILibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace FT_UILibrary.Endpoint
{
    public class FTHistoryEndpoint : IFTHistoryEndpoint
    {
        private readonly IAPIHelper _apiHelper;
        public FTHistoryEndpoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task PostHistory(FTHistoryModel history)
        {
            var data = new { history };

            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/History", data))
            {
                if (response.IsSuccessStatusCode)
                {
                    //log successful call
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
