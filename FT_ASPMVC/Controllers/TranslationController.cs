using FT_ASPMVC.Models;
using FT_UILibrary.Endpoint;
using FT_UILibrary.Models;
using FT_UILibrary.Models.UserModels;
using Microsoft.AspNetCore.Mvc;

namespace FT_ASPMVC.Controllers
{
    public class TranslationController : Controller
    {
        private readonly IFTEndpoint _translationEndpoint;
        private readonly IFTHistoryEndpoint _fTHistoryEndpoint;
        private readonly ILogger _logger;
        private ILoggedInUserModel _loggedInUser;

        public TranslationController(IFTEndpoint translationEndpoint
                                    , IFTHistoryEndpoint historyEndpoint
                                    , ILogger<TranslationController> logger
                                    , ILoggedInUserModel loggedInUser)
        {
            _translationEndpoint = translationEndpoint;
            _fTHistoryEndpoint = historyEndpoint;
            _logger = logger;
            _loggedInUser = loggedInUser;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Translate(TranslationRequestDisplayModel request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    // Handle validation errors
                    // Nothing written due to the limit of time


                    //Log the not valid request
                    _logger.LogInformation(
                        "Not Valid Translation Request: User {User} was trying to translate the following text: {RequestedText}",
                        _loggedInUser.Id, request);


                    return View("Index");
                }

                //Log valid request
                _logger.LogInformation(
                    "Valid Translation Request: User {User} is trying to translate the following text: {RequestedText}",
                    _loggedInUser.Id, request);


                var translation = await _translationEndpoint.TranslateText(request.Text);


                TranslationResponseDisplayModel response = new()
                {
                    TranslatedText = translation.TranslatedText
                };

                if (translation != null)
                {
                    // Save the translation and request details to the database or log them for analysis
                    // We can use a database context or any logging framework of your choice
                    //Log the failed request
                    _logger.LogInformation(
                        "Translation Response: User {User} got translate response: {TranslatedText} for the request text {Request}",
                        _loggedInUser.Id, translation.TranslatedText, request.Text);

                    //TODO: log to the database
                    FTHistoryModel history = new()
                    {
                        RequestText = request.Text,
                        Response = translation.TranslatedText,
                        BaseApiUsed = _translationEndpoint.EndpointUrl,
                        EndpointUsed = _translationEndpoint.BaseApiUrl
                    };
                    SaveHistory(history);

                    return View("Result", response);
                }
            }
            catch (Exception)
            {
                // Handle translation failure
                //Log the failed request
                _logger.LogInformation(
                    "Failed Translation Request: User {User} failed to translate the following text: {RequestedText}",
                    _loggedInUser.Id, request);

            }

            return View("Index");
        }

        private void SaveHistory(FTHistoryModel history)
        {
            try
            {
                _fTHistoryEndpoint.PostHistory(history);
            }
            catch (Exception)
            {
                //TODO: Handle failure
                //...
            }
        }
    }
}
