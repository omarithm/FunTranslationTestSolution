using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FT_ApiLibrary.Models
{
    public class FTApiPropertiesModel
    {
        public int Id { get; set; }
        public string FunTranslationsApiBaseUrl { get; set; }
        public string TranslationEndpoint { get; set; }
        public string? FunTranslationsApiKey { get; set; }
    }
}
