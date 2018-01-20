using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Events.Bus;
using Abp.UI;
using Don2018.EventCloud.Authorization.Users;
using Microsoft.EntityFrameworkCore;

namespace Don2018.EventCloud.Domain.Events
{
    public class EventManager : IEventManager
    {
        private readonly IEventBus _eventBus;

        private readonly IEventRegistrationPolicy _registrationPolicy;
        private readonly IRepository<EventRegistration> _eventRegRepo;
        private readonly IRepository<Event, Guid> _eventRepo;
      


        public EventManager(IEventRegistrationPolicy registrationPolicy, IRepository<EventRegistration> eventRegRepo, IRepository<Event, Guid> eventRepo)
        {
            _registrationPolicy = registrationPolicy;
            _eventRegRepo = eventRegRepo;
            _eventRepo = eventRepo;

            _eventBus = NullEventBus.Instance;
        }

        public async Task<Event> GetAsync(Guid id)
        {
            var @event = await _eventRepo.FirstOrDefaultAsync(id);
            if (@event == null)
            {
                throw new UserFriendlyException("Could not found the event, maybe it's deleted");
            }
            return @event;
        }

        public async Task CreateAsync(Event @event)
        {
            await _eventRepo.InsertAsync(@event);
        }

        public void Cancel(Event @event)
        {
            @event.Cancel();
            _eventBus.Trigger(new EventCancelledEvent(@event));
        }

        public async Task<EventRegistration> RegisterAsync(Event @event, User user)
        {
            return await _eventRegRepo.InsertAsync(
                await EventRegistration.CreateAsync(@event, user, _registrationPolicy));
        }

        public async Task CancelRegistrationAsync(Event @event, User user)
        {
            var registration =
                await _eventRegRepo.FirstOrDefaultAsync(r => r.EventId == @event.Id && r.UserId == user.Id);

            if (registration == null)
            {
                //No need to cancel since there is no such a registration
                //UserfriendlyException here?
                return;
            }
            await registration.CancelAsync(_eventRegRepo);
        }

        public async Task<IReadOnlyList<User>> GetRegisteredUsersAsync(Event @event)
        {
            return await _eventRegRepo.GetAll()
                .Include(registration => registration.User)
                .Where(registration => registration.EventId == @event.Id)
                .Select(registration => registration.User)
                .ToListAsync();
        }
    }
}

/*EventManager implements business (domain) logic for events. All Event operations should be executed using this class. It's defined as shown below:*/
