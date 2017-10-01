using Events.ApplicationService;
using Events.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Events.Models;

namespace Events.Web.Controllers
{
	[Authorize]
    public class BaseController : Controller
    {
        protected IEventsAppService _eventsAppService = null;

        public BaseController()
        {
            string userName = string.Empty;
            if (System.Web.HttpContext.Current.User != null)
            {
                userName = System.Web.HttpContext.Current.User.Identity.Name;
            }
            this._eventsAppService = new EventsAppService(userName);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

        }
    }
}