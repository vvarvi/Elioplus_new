using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Configuration;
using WdS.ElioPlus.Lib.Utils;

namespace WdS.ElioPlus.Lib.Services.CRMs.Dynamics365API
{
    public class CrmSeviceHelper
    {
        public static CrmServiceClient Connect(string name, string clientId, string clientSecret)
        {
            CrmServiceClient service = null;

            //You can specify connection information in web.config
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(clientSecret))
            {
                string connectionString = GetConnectionStringFromAppConfig(name).Replace("{ClientId}", clientId).Replace("{ClientSecret}", clientSecret);
                
                // Try to create via connection string. 
                service = new CrmServiceClient(connectionString);
            }

            return service;

        }

        private static string GetConnectionStringFromAppConfig(string name)
        {
            //Verify cds/App.config contains a valid connection string with the name.
            try
            {
                return ConfigurationManager.ConnectionStrings[name].ConnectionString;
            }
            catch (Exception)
            {
                Logger.DetailedError("WdS.ElioPlus.Lib.Services.CRMs.Dynamics365API.CrmSeviceHelper.cs --> GetConnectionStringFromAppConfig()", "ConnectionString to Dynamics 365 CRM could not be done!");
                return string.Empty;
            }
        }
    }
}