using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RaghavWorkshop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            string strToken = GetAuthToken();
        }

        static string serviceUri = "https://cfxcedev3.crm.dynamics.com/";
        static string redirectUrl = "https://cfxcedev3.api.crm.dynamics.com/XRMServices/2011/Organization.svc";

        public static string GetAuthToken()
        {

            // TODO Substitute your app registration values that can be obtained after you
            // register the app in Active Directory on the Microsoft Azure portal.
            string clientId = "6267eed1-0145-421f-9537-c07cd4a39ac2`"; // Client ID after app registration
            string userName = "gopalr@cfxway.com";
            string password = "CFXway2020?";
            UserCredential cred = new UserCredential(userName, password);

            // Authenticate the registered application with Azure Active Directory.
            AuthenticationContext authContext = new AuthenticationContext("login.windows.net/common", false);
            AuthenticationResult result = authContext.AcquireToken(serviceUri, clientId, cred);
            return result.AccessToken;
        }
        public static void RetrieveAccounts(string authToken)
        {
            HttpClient httpClient = null;
            httpClient = new HttpClient();
            //Default Request Headers needed to be added in the HttpClient Object
            httpClient.DefaultRequestHeaders.Add("OData-MaxVersion", "4.0");
            httpClient.DefaultRequestHeaders.Add("OData-Version", "4.0");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //Set the Authorization header with the Access Token received specifying the Credentials
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

            httpClient.BaseAddress = new Uri(redirectUrl);
            var response = httpClient.GetAsync("accounts?$select=name").Result;
            if (response.IsSuccessStatusCode)
            {
                var accounts = response.Content.ReadAsStringAsync().Result;
            }
        }
    }
}
