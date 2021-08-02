using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using CfxAlertJob.Model;
using Newtonsoft.Json;
using System.Web;
using Microsoft.JScript;

namespace CfxAlertJob
{
    public class CrmService : ICrmService
    {        
        #region Private variables

        string _crmURL = string.Empty;
        string _apiEndPoint = string.Empty;
        string _clientId = string.Empty;
        string _clientSecret = string.Empty;
        string _tenantId = string.Empty;
        string _azureLoginURL = string.Empty;

        #endregion

        public readonly IConfiguration _config;        

        public CrmService()
        {
            
            #region initialize configuration values

            _crmURL = "https://cfxcedev3.api.crm.dynamics.com/";
            _apiEndPoint = "api/data/v9.1/";
            _clientId = "bc0e63cf-abfa-4ad7-b6eb-226a8c610257";
            _clientSecret = "C_9cJ1_5x49-lZ906718dq2rH.~T9TlUrg";
            _tenantId = "5d25b04c-c06d-4cf3-8436-843332775f59";
            _azureLoginURL = "https://login.microsoftonline.com/";

            #endregion
        }

        //Retrieve entities based on the oData query passed
        public HttpResponseMessage Retrieve(string oDataRequest)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {                
                string requestUri = _crmURL + _apiEndPoint;
                HttpClient client = GenerateHttpClient();
                response = client.GetAsync(requestUri + oDataRequest, HttpCompletionOption.ResponseHeadersRead).Result;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
            }
            return response;
        }

        //Retrieve entities based on the Entityname & GUID passed
        public HttpResponseMessage Retrieve(string entityName, string id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                string oDataRequest = entityName + "s?$filter=" + entityName + "id eq " + id;
                string requestUri = _crmURL + _apiEndPoint;
                HttpClient client = GenerateHttpClient();
                response = client.GetAsync(requestUri + oDataRequest, HttpCompletionOption.ResponseHeadersRead).Result;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
            }
            return response;
        }

        public CrmServiceResponse Create(string EntityName, JObject JsonEntity)
        {           
            CrmServiceResponse crmResponse = new CrmServiceResponse();
            try
            {
                string requestUri = _crmURL + _apiEndPoint + EntityName;
                HttpClient client = GenerateHttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestUri);
                request.Content = new StringContent(JsonEntity.ToString());
                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                crmResponse.Response = client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).Result;
                if (!crmResponse.Response.IsSuccessStatusCode)
                {
                    crmResponse.IsSuccess = false;
                    crmResponse.ExceptionDetails = crmResponse.Response.ReasonPhrase;
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
            }
            return crmResponse;
        }

        public CrmServiceResponse Update(string EntityName, string Id, string strObject)
        {
            CrmServiceResponse crmResponse = new CrmServiceResponse();
            try
            {
                AlertMaster objAlertMaster = new AlertMaster();
                objAlertMaster.LastRun = DateTime.Now.ToString();
                string requestUri = _crmURL + _apiEndPoint + EntityName+"("+Id+")";                
                HttpClient client = GenerateHttpClient();
                HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("PATCH"), requestUri);
                request.Content = new StringContent(strObject, Encoding.UTF8, "application/json");                
                crmResponse.Response = client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).Result;
                if (!crmResponse.Response.IsSuccessStatusCode)
                {
                    crmResponse.IsSuccess = false;
                    crmResponse.ExceptionDetails = crmResponse.Response.ReasonPhrase;
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
            }
            return crmResponse;
        }

        public CrmServiceResponse ExecuteFetchXML(string EntityPluralName, string FetchXML)
        {
            CrmServiceResponse crmResponse = new CrmServiceResponse();
            try
            {
                string requestUri = _crmURL + _apiEndPoint;
                HttpClient client = GenerateHttpClient();
                crmResponse.Response= client.GetAsync(requestUri + EntityPluralName+ "?fetchXml="+ GlobalObject.encodeURIComponent(FetchXML), HttpCompletionOption.ResponseHeadersRead).Result;
                if (!crmResponse.Response.IsSuccessStatusCode)
                {
                    crmResponse.IsSuccess = false;
                    crmResponse.ExceptionDetails = crmResponse.Response.ReasonPhrase;
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
            }
            return crmResponse;
        }
        private HttpClient GenerateHttpClient()
        {
            HttpClient httpClient = new HttpClient();
            try
            {

                //Default Request Headers needed to be added in the HttpClient Object
                httpClient.DefaultRequestHeaders.Add("OData-MaxVersion", "4.0");
                httpClient.DefaultRequestHeaders.Add("OData-Version", "4.0");
                //httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json;odata.metadata=none"));
                //Set the Authorization header with the Access Token received specifying the Credentials
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetD365AccessToken());
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
            }
            return httpClient;
        }

        //Generate access token for D365 CE based with Azure credentials
        private string GetD365AccessToken()
        {

            string AccessToken = string.Empty;

            try
            {
                ClientCredential clientCredential = new ClientCredential(_clientId, _clientSecret);
                AuthenticationResult authenticationResult = new AuthenticationContext(_azureLoginURL + _tenantId).AcquireTokenAsync(_crmURL, clientCredential).Result;
                AccessToken = authenticationResult == null ? string.Empty : authenticationResult.AccessToken;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
            }
            return AccessToken;
        }
    }
}

