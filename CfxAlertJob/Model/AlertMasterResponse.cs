using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfxAlertJob.Model
{
    public class AlerMasterResponse
    {
        public List<AlertMaster> value { get; set; }
    }

    public class AlertMaster
    {
        public string cfx_alertmasterid { get; set; }
        public string cfx_alerttype { get; set; }
        public string cfx_enddate { get; set; }
        public string cfx_startdate { get; set; }

        [JsonProperty(PropertyName = "cfx_lastrun")]
        public string LastRun { get; set; }

        public string cfx_fetchxml { get; set; }
        public string _cfx_from_value { get; set; }
        public string cfx_intervalnumber { get; set; }
        public string cfx_intervaltype { get; set; }
        
        public string cfx_messagetext { get; set; }

        [JsonProperty(PropertyName = "cfx_name")]
        public string AlertName { get; set; }
        public string cfx_recurring { get; set; }
        public string cfx_severity { get; set; }
        
        public string statecode { get; set; }

        public string cfx_communicationpreference { get; set; }
    }

    
}
