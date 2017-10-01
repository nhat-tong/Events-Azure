using Events.Web.Helpers;
using Events.Web.Mappers;
using Events.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace Events.Web.Controllers
{
    public class EventsController : BaseController
    {
        //
        // GET: /Events/
        async public Task<ActionResult> Index()
        {
            var user = (await this._eventsAppService.ActiveDirectoryUsersAsync()).Where(u => u.ActiveDirectoryId == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();
            if (string.IsNullOrWhiteSpace(user.ImageUrl))
            {
                user.ImageUrl = Url.Content("~/Images/user-placeholder.png");
            }
            ViewBag.CurrentUser = user;
            return View();
        }

        async public Task<ActionResult> AddEvent()
        {
            EventViewModel viewModel = new EventViewModel();
            viewModel.Owner = System.Web.HttpContext.Current.User.Identity.Name;
            viewModel.OwnerId = viewModel.Owner;

            ViewBag.Users = await this.GetActiveUsers();
            ViewBag.CurrentView = MenuEnabledView.AddEvent;
            ViewBag.EventViewModel = viewModel;
            return View(viewModel);
        }

        [HttpPost]
        async public Task<ActionResult> AddEvent(EventViewModel addedEvent)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var eventData = addedEvent.MapToModel();
                    eventData = this._eventsAppService.CreateEvent(eventData);
                    if (eventData != null)
                    {
                        return RedirectToAction("SubmitSuccess", "Events", new { id = eventData.Id });
                    }
                }
                ViewBag.CurrentView = MenuEnabledView.AddEvent;
                ViewBag.Users = await this.GetActiveUsers();
                ViewBag.CurrentView = MenuEnabledView.AddEvent;
                ViewBag.DateRangeError = true;
                return View(addedEvent);
            }
            catch (Exception)
            {
                ViewBag.Users = await this.GetActiveUsers();
                ViewBag.CurrentView = MenuEnabledView.AddEvent;
                ModelState.AddModelError("Error", "Error Occured");
                return View();
            }
        }

        public ActionResult MyEvents()
        {
            ViewBag.CurrentView = MenuEnabledView.MyEvents;
            var events = this._eventsAppService.GetUserEvents(System.Web.HttpContext.Current.User.Identity.Name);
            return View(events.MapToViewModelCollection());
        }

        public ActionResult SubmitSuccess(int id)
        {
            string registerLink = this.Request.Url.AbsoluteUri.Replace(this.Request.Url.AbsolutePath, "/Events/Register/" + id.ToString());
            @ViewBag.EventRegistrationLink = registerLink;
            return View();
        }

        async public Task<ActionResult> Register(int id)
        {
            if (id > 0)
            {
                var eventData = this._eventsAppService.GetEvent(id);
                if (eventData == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                ViewBag.Users = await this.GetActiveUsers();
                var viewModel = eventData.MapToViewModel();
                viewModel.Owner = System.Web.HttpContext.Current.User.Identity.Name;
                viewModel.OwnerId = viewModel.Owner;
                ViewBag.CurrentView = MenuEnabledView.Default;
                return View(viewModel);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult RegisterSuccess(string AttendeeId, int EventId)
        {
            var userEvents = this._eventsAppService.GetUserEvents(AttendeeId);

            if (!userEvents.Any(ue => ue.Id == EventId))
                this._eventsAppService.RegisterUser(AttendeeId, EventId);

            return View();
        }

        async private Task<IEnumerable<ActiveDirectoryUser>> GetActiveUsers()
        {
            return await this._eventsAppService.ActiveDirectoryUsersAsync();
        }
    }
}