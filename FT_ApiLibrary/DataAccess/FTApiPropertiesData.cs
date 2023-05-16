using FT_ApiLibrary.Internal.DataAccess;
using FT_ApiLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FT_ApiLibrary.DataAccess
{
    public class FTApiPropertiesData : IFTApiPropertiesData
    {
        private readonly ISqlDataAccess _sql;

        public FTApiPropertiesData(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        public FTApiPropertiesModel GetFTApiProperties()
        {
            var output = _sql.LoadData<FTApiPropertiesModel, dynamic>("dbo.Proc_FTApiProperties_GetAll", new { }, "FunTranslationDB").FirstOrDefault();

            return output;
        }


        public void SaveFTApiProperties(FTApiPropertiesModel funTranslation)
        {
            FTApiPropertiesDBModel ftDBModel = new()
            {
                FunTranslationsApiBaseUrl = funTranslation.FunTranslationsApiBaseUrl,
                TranslationEndpoint = funTranslation.TranslationEndpoint,
                FunTranslationsApiKey = funTranslation.FunTranslationsApiKey
            };

            try
            {
                //Save in the database
                _sql.SaveData("dbo.Proc_FTApiProperties_Insert", ftDBModel, "FunTranslationDB");
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not save API Properties of the Fun Translation!\n{ex.Message}");
            }
        }

    }
}
