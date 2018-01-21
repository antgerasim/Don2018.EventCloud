using System;
using System.Linq;
using Don2018.EventCloud.Domain.Events;

namespace Don2018.EventCloud.EntityFrameworkCore.Seed.Host
{
    public class InitialEventsCreator
    {
        private readonly EventCloudDbContext _context;

        public InitialEventsCreator(EventCloudDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            var angularWorkShop = _context.Events.FirstOrDefault(e => e.Title == "Angularjs Workshop");
            if (angularWorkShop == null)
            {
                var title = "Angularjs Workshop";
                var description =
                    "In this 2 hours workshop, we will introduce Angular and build a simple todo application using...";
                //var date = new DateTime(2018, 10, 23);
                var date = DateTime.Today.AddDays(547);
                var maxRegistrationCount = 25;
                _context.Events.Add(Event.Create(0, title, date, description, maxRegistrationCount));
            }

            var istanbulStartupMeeting = _context.Events.FirstOrDefault(e => e.Title == "Istanbul Startup Meeting");
            if (istanbulStartupMeeting==null)
            {
                var title = "Istanbul Startup Meeting";
                var description =
                    "This is some description of the meeting.";
                //var date = new DateTime(2018, 11, 05);
                var date = DateTime.Today.AddDays(35);
                var maxRegistrationCount = 12;
                _context.Events.Add(Event.Create(0, title, date, description, maxRegistrationCount));
            }

            var udcLondon = _context.Events.FirstOrDefault(e => e.Title == "NDC London");
            if (udcLondon == null)
            {
                var title = "NDC London";
                var description =
                    "Inspiring Software Developers since 2008";
                //var date = new DateTime(2018, 02, 15);
                var date = DateTime.Today.AddDays(121);
                var maxRegistrationCount = 255;
                _context.Events.Add(Event.Create(0, title, date, description, maxRegistrationCount));
            }
        }
    }
}