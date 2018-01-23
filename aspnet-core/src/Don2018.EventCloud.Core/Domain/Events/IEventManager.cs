using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Services;
using Abp.Events.Bus;
using Don2018.EventCloud.Authorization.Users;

namespace Don2018.EventCloud.Domain.Events
{
    public interface IEventManager : IDomainService
    {
        void Cancel(Event @event);
        Task CancelRegistrationAsync(Event @event, User user);
        Task CreateAsync(Event @event);
        Task<Event> GetAsync(Guid id);
        Task<IReadOnlyList<User>> GetRegisteredUsersAsync(Event @event);
        Task<EventRegistration> RegisterAsync(Event @event, User user);
    }
}