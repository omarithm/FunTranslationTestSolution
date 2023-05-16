using FT_Api.Models;
using FT_ApiLibrary.DataAccess;
using FT_ApiLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FT_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //Do not forget to uncomment the [Authorize] after finish setup the Authorization UI
    //[Authorize]
    public class HistoryController : ControllerBase
    {
        private readonly IFTHistoryData _historyData;
        private readonly ILogger _logger;

        public HistoryController(IFTHistoryData historyData,
                                ILogger<HistoryController> logger)
        {
            _historyData = historyData;
            _logger = logger;
        }

        //TODO: Get history by search word
        //....

        [HttpPost]
        public async Task Post(FTHistoryModel history)
        {
            //To capture more information about the user 
            string loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier); 

            //Save History
            _historyData.SaveFTHistory(history, loggedInUserId);

            //Log the result
            _logger.LogInformation("History Saved: User {User} just saved translation request history",
                loggedInUserId);
        }
    }
}
