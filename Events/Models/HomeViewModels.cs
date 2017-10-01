using Events.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Events.Models
{
    public class HomeViewModel
    {
        public IEnumerable<EventViewModel> UpcomingEvents { get; set; }
    }

    public class UserProfile
    {
        public string DisplayName { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
    }
}