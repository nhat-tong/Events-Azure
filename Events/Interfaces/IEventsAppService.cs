using Events.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    public interface IEventsAppService
    {
        Event GetEvent(int eventId);

        IEnumerable<Event> UpcomingEvents(int count);

        IEnumerable<Event> GetUserEvents(string activeDirectoryId);

        Event CreateEvent(Event eventData);

        bool RegisterUser(string activeDirectoryId, int eventId);

        //IEnumerable<ActiveDirectoryUser> ActiveDirectoryUsers();

        //ActiveDirectoryUser ActiveDirectoryUser(string activeDirectoryId);


        Task<IEnumerable<ActiveDirectoryUser>> ActiveDirectoryUsersAsync();
    }
}