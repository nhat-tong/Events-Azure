using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OpenIdConnect;
using System.Data.Services.Client;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using Events.Helpers;

namespace Events.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        public async Task<ActionResult> ShowThumbnail(string id)
        {
            try
            {
                ActiveDirectoryClient client = AuthenticationHelper.GetActiveDirectoryClient();
                IUser user = await client.Users.GetByObjectId(id).ExecuteAsync();

                try
                {
                    DataServiceStreamResponse response = await user.ThumbnailPhoto.DownloadAsync();
                    if(response != null)
                    {
                        return File(response.Stream, "image/jpeg");
                    }
                }
                catch
                {
                    var file = Server.MapPath("~/Images/user-placeholder.png");
                    return File(file, "image/png", Path.GetFileName(file));
                }
            }
            catch
            {
            }

            return View();
        }
    }
}