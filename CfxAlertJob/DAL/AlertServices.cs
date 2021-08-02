using CfxAlertJob.Model;
using Microsoft.Xrm.Sdk;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfxAlertJob.DAL
{
    public class AlertServices
    {
        CrmService _crmService;

        public AlertServices()
        {
            _crmService = new CrmService();
        }
        public List<AlertMaster> GetAllActiveAlerts()
        {
            string strResponse = string.Empty;
            
            List<AlertMaster> lstAlertMasters = new List<AlertMaster>();
            CrmServiceResponse crmServiceResponse = new CrmServiceResponse();
            try
            {
                /*
                crmServiceResponse.Response = _crmService.Retrieve(AlertApiConstants.ActiceAlertMastersQuery.ToString());
                if (crmServiceResponse.IsSuccess)
                {
                    AlerMasterResponse objAlertResponse = new AlerMasterResponse();
                    strResponse = crmServiceResponse.Response.Content.ReadAsStringAsync().Result;
                    objAlertResponse = JsonConvert.DeserializeObject<AlerMasterResponse>(strResponse);
                    lstAlertMasters= objAlertResponse.value;
                }                
                */
                string FetchXML = AlertApiConstants.ActiceAlertMastersQuery.ToString();
                FetchXML = FetchXML.Replace("\"", "'");
                crmServiceResponse = _crmService.ExecuteFetchXML("cfx_alertmasters", FetchXML);
                if (crmServiceResponse.IsSuccess)
                {
                    AlerMasterResponse objJsonResponse = new AlerMasterResponse();
                    strResponse = crmServiceResponse.Response.Content.ReadAsStringAsync().Result;
                    objJsonResponse = JsonConvert.DeserializeObject<AlerMasterResponse>(strResponse);
                    lstAlertMasters = objJsonResponse.value;
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
                //retrieveUsernameResponse.ExceptionDetails = ex.Message;
            }
            return lstAlertMasters;
        }

        public bool UpdateLastRun(AlertMaster objAlertMaster) 
        {            
            bool blReturn = true;
            CrmServiceResponse crmServiceResponse = new CrmServiceResponse();
            try
            {
                //string strObject = JsonConvert.SerializeObject(objAlertMaster);
                string strObject = "{'cfx_lastrun':'"+DateTime.Now.ToString()+"'}";                
                crmServiceResponse = _crmService.Update(AlertApiConstants.AlertMasterEntityName,objAlertMaster.cfx_alertmasterid, strObject);
                if (!crmServiceResponse.IsSuccess)
                {
                    blReturn = false; ;
                }                
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
                //retrieveUsernameResponse.ExceptionDetails = ex.Message;
            }
            return blReturn;
        }

        public bool CreateAlertNotification(AlertMaster objAlertMaster, Account objAccount)
        {
            bool blReturn = true;
            CrmServiceResponse crmServiceResponse = new CrmServiceResponse();
            try
            {
                JObject jAlertNotification= new JObject();
                jAlertNotification.Add("cfx_name", objAlertMaster.AlertName);
                //jAlertNotification.Add("cfx_severity", objAlertMaster.cfx_severity);
                //jAlertNotification.Add("cfx_startdate", objAlertMaster.cfx_startdate);
                //jAlertNotification.Add("cfx_communicationpreference", objAlertMaster.cfx_communicationpreference);
                //jAlertNotification.Add("cfx_alertmessage", objAlertMaster.cfx_messagetext);
                //jAlertNotification.Add("regardingobjectid@odata.bind", "/accounts(" + objAccount.accountid + ")");
                jAlertNotification.Add("cfx_alert@odata.bind", "/cfx_alertmasters(" + objAlertMaster.cfx_alertmasterid +")");
                crmServiceResponse =_crmService.Create("cfx_alertnotifications", jAlertNotification);
                if (!crmServiceResponse.IsSuccess)
                {
                    blReturn = false; ;
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
                //retrieveUsernameResponse.ExceptionDetails = ex.Message;
            }
            return blReturn;
        }
    }
}
