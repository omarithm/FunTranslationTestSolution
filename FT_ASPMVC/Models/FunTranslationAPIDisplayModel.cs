using static System.Net.Mime.MediaTypeNames;

namespace FT_ASPMVC.Models
{
    public class FunTranslationAPIDisplayModel
    {
        public static string FunTranslationsApiBaseUrl { get; set; }
        public static string TranslationEndpoint { get; set; }
        public static string? FunTranslationsApiKey { get; set; }

    }
}
