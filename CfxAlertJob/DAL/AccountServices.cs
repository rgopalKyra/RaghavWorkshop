using CfxAlertJob.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfxAlertJob.DAL
{
    public class AccountServices
    {
        CrmService _crmService;

        public AccountServices()
        {
            _crmService = new CrmService();
        }

        public List<Account> GetAllActiveAccounts()
        {
            string strResponse = string.Empty;

            List<Account> lstAccounts = new List<Account>();
            CrmServiceResponse crmServiceResponse = new CrmServiceResponse();
            try
            {
                crmServiceResponse.Response = _crmService.Retrieve(AlertApiConstants.ActiveAccountsQuery.ToString());
                if (crmServiceResponse.IsSuccess)
                {
                    JsonValueResponse objAccountResponse = new JsonValueResponse();
                    strResponse = crmServiceResponse.Response.Content.ReadAsStringAsync().Result;
                    objAccountResponse = JsonConvert.DeserializeObject<JsonValueResponse>(strResponse);
                    lstAccounts = objAccountResponse.value;
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
                //retrieveUsernameResponse.ExceptionDetails = ex.Message;
            }
            return lstAccounts;
        }

        public List<Account> getAccountsByFetchXML(string FetchXML)
        {
            string strResponse = string.Empty;

            List<Account> lstAccounts = new List<Account>();
            CrmServiceResponse crmServiceResponse = new CrmServiceResponse();
            try
            {
                FetchXML = FetchXML.Replace("\"", "'");
                crmServiceResponse = _crmService.ExecuteFetchXML("accounts", FetchXML);
                if (crmServiceResponse.IsSuccess)
                {
                    JsonValueResponse objJsonResponse = new JsonValueResponse();
                    strResponse = crmServiceResponse.Response.Content.ReadAsStringAsync().Result;
                    objJsonResponse = JsonConvert.DeserializeObject<JsonValueResponse>(strResponse);
                    lstAccounts = objJsonResponse.value;
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
                //retrieveUsernameResponse.ExceptionDetails = ex.Message;
            }
            return lstAccounts;
        }
    }
}
