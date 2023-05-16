using FT_ApiLibrary.Models;

namespace FT_ApiLibrary.DataAccess
{
    public interface IFTHistoryData
    {
        List<FTHistoryModel> GetFTHistoryById(string userId);
        void SaveFTHistory(FTHistoryModel history, string userId);
    }
}