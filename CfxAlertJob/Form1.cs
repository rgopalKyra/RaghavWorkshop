using CfxAlertJob.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CfxAlertJob
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AlertAgent objAgent = new AlertAgent();
            objAgent.StartService();
            //   CrmService objCrmService = new CrmService();
            //string strQuery = "<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>  <entity name='account'>    <attribute name='name' />    <attribute name='primarycontactid' />    <attribute name='telephone1' />    <attribute name='accountid' />    <attribute name='cfx_noofactivealerts' />    <order attribute='name' descending='false' />    <filter type='and'>      <condition attribute='accountid' operator='eq' value='{82ef14c5-ee96-eb11-b1ac-000d3a1bd6dd}' />    </filter>  </entity></fetch>";
            //CrmServiceResponse objResponse= objCrmService.ExecuteFetchXML("accounts", strQuery);
            //string strResponse = objResponse.Response.Content.ReadAsStringAsync().Result;
            //MessageBox.Show("done");
        }
    }
}
