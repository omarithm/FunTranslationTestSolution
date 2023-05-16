using FT_ApiLibrary.Internal.DataAccess;
using FT_ApiLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FT_ApiLibrary.DataAccess
{
    public class FTHistoryData : IFTHistoryData
    {
        private readonly ISqlDataAccess _sql;
        public FTHistoryData(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        public List<FTHistoryModel> GetFTHistoryById(string userId)
        {
            var output = _sql.LoadData<FTHistoryModel, dynamic>("dbo.Proc_FTHistory_GetById", new { userId }, "FunTranslationDB");

            return output;
        }

        //TODO: Get history by search word
        //Requires create stored procedure
        //...

        public void SaveFTHistory(FTHistoryModel history, string userId)
        {
            FTHistoryDBModel ftDBModel = new()
            {
                CreatedById = userId,
                CreatedDate = history.CreatedDate,
                RequestText = history.RequestText,
                Response = history.Response,
                BaseApiUsed = history.BaseApiUsed,
                EndpointUsed = history.EndpointUsed,
            };

            try
            {
                //Save in the database
                _sql.SaveData("dbo.Proc_FTHistory_Insert", ftDBModel, "FunTranslationDB");
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not save Fun Translation History!\n{ex.Message}");
            }
        }
    }
}
