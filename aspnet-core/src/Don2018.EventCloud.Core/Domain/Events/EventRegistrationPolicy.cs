using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Repositories;
using Abp.Timing;
using Abp.UI;
using Don2018.EventCloud.Authorization.Users;
using Don2018.EventCloud.Configuration;
using Abp.Configuration;

namespace Don2018.EventCloud.Domain.Events
{
    public class EventRegistrationPolicy : AbpServiceBase, IEventRegistrationPolicy
    {
        private readonly IRepository<EventRegistration> _eventRegRepo;

        public EventRegistrationPolicy(IRepository<EventRegistration> eventRegRepo)
        {
            _eventRegRepo = eventRegRepo;
        }

        public async Task CheckRegistrationAttemptAsync(Event @event, User user)
        {
            if (@event == null)
                throw new ArgumentNullException("event");

            if (user == null)
                throw new ArgumentNullException("user");

            CheckEventDate(@event); // is in past?
            await CheckEventRegistrationFrequencyAsync(user);
        }

/*        public async Task CheckRegistrationAttemptAsync(Event @event, User user)
        {
            throw new NotImplementedException();
        }*/

        private async Task CheckEventRegistrationFrequencyAsync(User user)
        {
            var oneMonthAgo = Clock.Now.AddDays(-30);

            var maxAllowedEventRegistrationCountInLast30DaysPerUser =
                await SettingManager.GetSettingValueAsync<int>(AppSettingNames
                    .MaxAllowedEventRegistrationCountInLast30DaysPerUser);

            if (maxAllowedEventRegistrationCountInLast30DaysPerUser > 0)
            {
                var registrationCountInLast30Days =
                    await _eventRegRepo.CountAsync(r => r.UserId == user.Id && r.CreationTime >= oneMonthAgo);

                if (registrationCountInLast30Days > maxAllowedEventRegistrationCountInLast30DaysPerUser)
                {
                    throw new UserFriendlyException(string.Format("Cannot register to more then {0}",
                        maxAllowedEventRegistrationCountInLast30DaysPerUser));
                }
            }
        }

        private static void CheckEventDate(Event @event)
        {
            if (@event.IsInPast())
                throw new UserFriendlyException("Cannot register event in the past");
        }
    }
}