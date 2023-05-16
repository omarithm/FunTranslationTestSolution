using FT_UILibrary.Models;

namespace FT_UILibrary.Endpoint
{
    public interface IFTHistoryEndpoint
    {
        Task PostHistory(FTHistoryModel history);
    }
}