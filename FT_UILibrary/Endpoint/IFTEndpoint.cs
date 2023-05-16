using FT_UILibrary.Models;

namespace FT_UILibrary.Endpoint
{
    public interface IFTEndpoint
    {
        Task<FTResponseModel> TranslateText(string text);
        string EndpointUrl { get; }
        string BaseApiUrl { get; }
    }
}