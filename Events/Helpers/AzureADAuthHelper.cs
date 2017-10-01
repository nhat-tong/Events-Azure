using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace Events.Helpers
{
    internal class AuthenticationHelper
    {
        public static string TokenForUser;

        /// <summary>
        /// Async task to acquire token for Application.
        /// </summary>
        /// <returns>Async Token for application.</returns>
        public static async Task<string> AcquireTokenAsyncForApplication()
        {
            return await GetTokenForApplication();
        }

        /// <summary>
        /// Get Token for Application.
        /// </summary>
        /// <returns>Token for application.</returns>
        public static async Task<string> GetTokenForApplication()
        {
            AuthenticationContext authenticationContext = new AuthenticationContext(AzureADConstants.AuthString, false);
            // Config for OAuth client credentials 
            ClientCredential clientCred = new ClientCredential(AzureADConstants.ClientId, AzureADConstants.ClientSecret);
            AuthenticationResult authenticationResult = await authenticationContext.AcquireTokenAsync(AzureADConstants.ResourceUrl,
                clientCred);
            return authenticationResult.AccessToken;
        }

       
        public static ActiveDirectoryClient GetActiveDirectoryClient()
        {
            Uri servicePointUri = new Uri(AzureADConstants.ResourceUrl);
            Uri serviceRoot = new Uri(servicePointUri, AzureADConstants.TenantId);
            ActiveDirectoryClient activeDirectoryClient = new ActiveDirectoryClient(serviceRoot,
                async () => await AcquireTokenAsyncForApplication());
            return activeDirectoryClient;
        }

    }
}