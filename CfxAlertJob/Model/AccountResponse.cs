using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfxAlertJob.Model
{
    public class JsonValueResponse
    {
        public List<Account> value { get; set; }
    }

    public class Account
    {
        public string accountid { get; set; }
        public string name { get; set; }        
    }
}
