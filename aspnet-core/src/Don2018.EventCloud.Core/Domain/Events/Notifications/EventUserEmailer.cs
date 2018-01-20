using System.Linq;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;
using Abp.Threading;
using Castle.Core.Logging;
using Don2018.EventCloud.Authorization.Users;

namespace Don2018.EventCloud.Domain.Events.Notifications
{
    public class EventUserEmailer : IEventHandler<EntityCreatedEventData<Event>>, IEventHandler<EventDateChangedEvent>,
        IEventHandler<EventCancelledEvent>, ITransientDependency
    {
        private readonly UserManager _userManager;
        private readonly IEventManager _eventManager;
        public ILogger Logger { get; set; }

        public EventUserEmailer(UserManager userManager, IEventManager eventManager)
        {
            _userManager = userManager;
            _eventManager = eventManager;

            Logger = NullLogger.Instance;
        }

        /// <summary>
        /// Event creation
        /// </summary>
        /// <param name="eventData"></param>
        [UnitOfWork]
        public void HandleEvent(EntityCreatedEventData<Event> eventData)
        {
            //Todo Send email to all tenant users as a notification

            var users = _userManager.Users.Where(u => u.TenantId == eventData.Entity.TenantId).ToList();

            foreach (var user in users)
            {
                var message = string.Format("Hey! There is a new event '{0}' on {1}! Want to register?",
                    eventData.Entity.Title, eventData.Entity.Date);

                Logger.Debug(string.Format("TODO: Send email to {0} -> {1}", user.EmailAddress, message));
            }
        }
        /// <summary>
        /// Event modification
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(EventDateChangedEvent eventData)
        {
            //Todo Send email to all registered users!

            var registeredUsers = AsyncHelper.RunSync(() => _eventManager.GetRegisteredUsersAsync(eventData.Entity));

            foreach (var regUser in registeredUsers)
            {
                var message = eventData.Entity.Title + " event's date is changed! New date is: " +
                              eventData.Entity.Date;
                Logger.Debug(string.Format("TODO: Send email to {0} -> {1}", regUser.EmailAddress, message));
            }
        }
        /// <summary>
        /// Event cancellation
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(EventCancelledEvent eventData)
        {
            //Todo: Send email to all registered users!
            var registeredUsers = AsyncHelper.RunSync(() => _eventManager.GetRegisteredUsersAsync(eventData.Entity));

            foreach (var regUser in registeredUsers)
            {
                var message = eventData.Entity.Title + " event is cancelled!";
                Logger.Debug(string.Format("TODO: Send email to {0} -> {1}", regUser.EmailAddress, message));
            }
        }
    }
}