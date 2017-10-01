using Events.ApplicationService;
using Events.Web.Helpers;
using Events.Web.Mappers;
using Events.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Events.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController()
        {
        }

        async public Task<ActionResult> Index()
        {
            var homeModel = new HomeViewModel();
            var cs = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            var matches = new Regex("^.*User Id=([^;]+);.*Password=([^;]+);?.*$", RegexOptions.Compiled | RegexOptions.IgnoreCase).Matches(cs);
            ViewBag.ConnectionString = cs.Replace(matches[0].Groups[1].Value, "[USER]")
                                      .Replace(matches[0].Groups[2].Value, "[PASSWORD]");
           
            var upcomingEvents = this._eventsAppService.UpcomingEvents(10);
            homeModel.UpcomingEvents = upcomingEvents.MapToViewModelCollection();
            ViewBag.CurrentView = MenuEnabledView.Default;

            //var audClaim = System.Web.HttpContext.Current.GetOwinContext().Authentication.User.Claims.Where(c => c.Type == "aud").FirstOrDefault();
            var objectIdVal = ClaimsPrincipal.Current.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
            var user = (await this._eventsAppService.ActiveDirectoryUsersAsync()).Where(u => u.ObjectId == objectIdVal).FirstOrDefault();
            if (string.IsNullOrWhiteSpace(user.ImageUrl))
            {
                user.ImageUrl = Url.Content("~/Images/user-placeholder.png");
            }
            ViewBag.CurrentUser = user;

            return View(homeModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}