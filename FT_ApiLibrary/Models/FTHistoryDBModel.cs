using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FT_ApiLibrary.Models
{
    public class FTHistoryDBModel
    {
        public int Id { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string RequestText { get; set; }
        public string Response { get; set; }
        public string BaseApiUsed { get; set; }
        public string EndpointUsed { get; set; }
    }
}
