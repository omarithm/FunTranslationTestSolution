using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FT_UILibrary.Models
{
    public class FTHistoryModel
    {
        public int Id { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public string RequestText { get; set; }
        public string Response { get; set; }
        public string BaseApiUsed { get; set; }
        public string EndpointUsed { get; set; }

    }
}