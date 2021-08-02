using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CfxAlertJob.Model
{
    public class CrmServiceResponse
    {
        public HttpResponseMessage Response { get; set; }
        
        public bool IsSuccess { get; set; } = true;
        
        public string ExceptionDetails { get; set; }
    }
}
    