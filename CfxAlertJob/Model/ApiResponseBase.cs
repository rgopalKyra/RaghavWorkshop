using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfxAlertJob.Model
{
    public class ApiResponseBase
    {
        public string Success { get; set; }
        public string ExceptionDetails { get; set; }
    }
}
