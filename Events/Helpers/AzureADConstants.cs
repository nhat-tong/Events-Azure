using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Events.Helpers
{
    internal class AzureADConstants
    {
        public static string ResourceUrl = "https://graph.windows.net";
        public static string ClientId = ConfigurationManager.AppSettings["ida:ClientId"];
        public static string AppKey = ConfigurationManager.AppSettings["ida:AppKey"];
        public static string TenantId = ConfigurationManager.AppSettings["ida:TenantId"];
        public static string AuthString = ConfigurationManager.AppSettings["ida:AADInstance"] + ConfigurationManager.AppSettings["ida:TenantId"];
        public static string ClientSecret = ConfigurationManager.AppSettings["ida:ClientSecret"];
        public static string RedirectUri = ConfigurationManager.AppSettings["ida:PostLogoutRedirectUri"];
    }
}