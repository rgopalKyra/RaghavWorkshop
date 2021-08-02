using CfxAlertJob.DAL;
using CfxAlertJob.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CfxAlertJob
{
    public class AlertAgent
    {
        CrmService objCrmService = new CrmService();
        public AlertAgent()
        {
            
        }

        public void StartService()
        {

            string strFetchXML = "";
            //"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>  <entity name='account'>    <attribute name='name' />    <attribute name='accountid' />      </entity></fetch>";
            
            AlertServices objAlertService = new AlertServices();
            AccountServices objAccountService = new AccountServices();            

            //Get the list of active Alert Master records 
            List<AlertMaster> lstAlertMaster= objAlertService.GetAllActiveAlerts();
            foreach (AlertMaster objAlertMaster in lstAlertMaster)
            {
                DateTime dtToday = DateTime.Parse(DateTime.Now.ToShortDateString());
                DateTime dtStartDate = new DateTime();
                DateTime dtEndDate = new DateTime();
                if (objAlertMaster.cfx_startdate!= null)
                    dtStartDate = DateTime.Parse(objAlertMaster.cfx_startdate.ToString());
                if (objAlertMaster.cfx_enddate != null)
                    dtEndDate = DateTime.Parse(objAlertMaster.cfx_enddate.ToString());
                //Check the End date >=  Now && Start Date <= Now
                if (objAlertMaster.cfx_startdate != null && dtStartDate <= dtToday && (dtEndDate >= dtToday || objAlertMaster.cfx_enddate ==  null))
                {
                    //Recurrsion == false
                    if (objAlertMaster.cfx_recurring == "224610001")
                    {
                        //Check if alert typle Simple
                        if (objAlertMaster.cfx_alerttype == "224610001")
                        {
                            //Get the message to send
                            string strMessage = objAlertMaster.cfx_messagetext;
                            //Get all active Accounts
                            List<Account> lstAccounts = objAccountService.GetAllActiveAccounts();
                            //Loop through Active Account
                            foreach (Account objAccount in lstAccounts)
                            {
                                objAlertService.CreateAlertNotification(objAlertMaster, objAccount);
                            }
                            //Update Last Run                            
                            objAlertService.UpdateLastRun(objAlertMaster);
                        }
                        //Check if the Alert Type is advaced
                        else if (objAlertMaster.cfx_alerttype == "224610000")
                        {
                            //Get the FetchXML
                            strFetchXML = objAlertMaster.cfx_fetchxml;
                            //Execute the Fetch XML                            
                            List<Account> lstAccounts = objAccountService.getAccountsByFetchXML(strFetchXML);
                            //Loop through Active Account
                            foreach (Account objAccount in lstAccounts)
                            {
                                //Create Alert
                                objAlertService.CreateAlertNotification(objAlertMaster, objAccount);

                            }
                            //Update last Run
                            objAlertService.UpdateLastRun(objAlertMaster);                            
                        }                                            
                    }// Recurrsion == true
                    else if (objAlertMaster.cfx_recurring == "224610000")
                    {
                        //Check Last Run == null or Last Run + interval number >= now
                        //Check if alert typle Simple
                        if (objAlertMaster.cfx_alerttype == "224610001")
                        {
                            //Get the message to send
                            string strMessage = objAlertMaster.cfx_messagetext;
                            //Get all active Accounts
                            List<Account> lstAccounts = objAccountService.GetAllActiveAccounts();
                            //Loop through Active Account
                            foreach (Account objAccount in lstAccounts)
                            {
                                //Create Alert
                                objAlertService.CreateAlertNotification(objAlertMaster, objAccount);

                            }
                            //Update Last Run                            
                            objAlertService.UpdateLastRun(objAlertMaster);
                        }//Check if the Alert Type is advaced
                        else if (objAlertMaster.cfx_alerttype == "224610000")
                        {

                            //Get the FetchXML
                            strFetchXML = objAlertMaster.cfx_fetchxml;
                            //Execute the Fetch XML                            
                            List<Account> lstAccounts = objAccountService.getAccountsByFetchXML(strFetchXML);
                            //Loop through Active Account
                            foreach (Account objAccount in lstAccounts)
                            {
                                //Create Alert
                                objAlertService.CreateAlertNotification(objAlertMaster, objAccount);

                            }
                            //Update last Run
                            objAlertService.UpdateLastRun(objAlertMaster);                            
                        }
                    }
                }
            }
        }        
    }
}
