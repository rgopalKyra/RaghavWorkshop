using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfxAlertJob.Model
{
    public class AlertApiConstants
    {
        public static string ActiceAlertMastersQuery = "<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'><entity name='cfx_alertmaster'><attribute name='cfx_alertmasterid' /><attribute name='cfx_name' /><attribute name='createdon' /><attribute name='cfx_alerttype' /><attribute name='statuscode' /><attribute name='statecode' /><attribute name='cfx_startdate' /><attribute name='cfx_severity' /><attribute name='cfx_recurring' /><attribute name='cfx_messagetext' /><attribute name='cfx_lastrun' /><attribute name='cfx_intervaltype' /><attribute name='cfx_intervalnumber' /><attribute name='cfx_from' /><attribute name='cfx_fetchxml' /><attribute name='cfx_enddate' /><filter type='and'><condition attribute='cfx_alerttype' operator='in'><value>224610000</value><value>224610001</value></condition><condition attribute='statecode' operator='eq' value='0' /></filter></entity></fetch>"; //"cfx_alertmasters?$select=cfx_alertmasterid,cfx_alerttype,cfx_enddate,cfx_fetchxml,_cfx_from_value,cfx_intervalnumber,cfx_intervaltype,cfx_lastrun,cfx_messagetext,cfx_name,cfx_recurring,cfx_severity,cfx_startdate,statecode,cfx_communicationpreference";
        public static string ActiveAccountsQuery = "/accounts?$select=accountid&$filter=statecode eq 0";

        public static string AlertMasterEntityName = "cfx_alertmasters";
    }
}
