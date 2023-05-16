namespace FT_UILibrary.API
{
    public interface IFTAPIHelper
    {
        HttpClient ApiClient { get; }
        string BaseApiUrl { get; }
    }
}