using FT_ApiLibrary.Models;

namespace FT_ApiLibrary.DataAccess
{
    public interface IFTApiPropertiesData
    {
        FTApiPropertiesModel GetFTApiProperties();
        void SaveFTApiProperties(FTApiPropertiesModel funTranslation);
    }
}