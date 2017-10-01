namespace Events
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Events.Models;

    public interface IEventsRepository
    {
        Event GetEvent(int eventId);

        IEnumerable<Event> UpcomingEvents(int count);

        IEnumerable<Event> GetUserEvents(string activeDirectoryId);

        Event CreateEvent(Event @event);

        bool RegisterUser(string activeDirectoryId, int eventId);

        //IEnumerable<ActiveDirectoryUser> ActiveDirectoryUsers();

        //ActiveDirectoryUser ActiveDirectoryUser(string activeDirectoryId);
    }
}
