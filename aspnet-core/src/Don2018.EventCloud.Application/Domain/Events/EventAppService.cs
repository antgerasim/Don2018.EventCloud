using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using Don2018.EventCloud.Authorization.Users;
using Don2018.EventCloud.Domain.Events.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Don2018.EventCloud.Domain.Events
{
    [AbpAuthorize]
    public class EventAppService : EventCloudAppServiceBase, IEventAppService
    {
        private readonly IEventManager _eventManager;
        private readonly IRepository<Event, Guid> _eventRepo;

        public EventAppService(IEventManager eventManager, IRepository<Event, Guid> eventRepo)
        {
            _eventManager = eventManager;
            _eventRepo = eventRepo;
        }

        public async Task<ListResultDto<EventListDto>> GetEvents(GetEventsInpput input)
        {
            var events = await _eventRepo
                .GetAll()
                .Include(e => e.Registrations) //use include for better performance (no lazy loading Registrations here)
                .WhereIf(!input.IncludeCancelledEvents, e => !e.IsCancelled)
                .OrderByDescending(e => e.CreationTime)
                .ToListAsync();

            var retVal = new ListResultDto<EventListDto>(events.MapTo<List<EventListDto>>());
            
            return retVal;
        }

        public async Task<EventDetailOutput> GetDetail(EntityDto<Guid> input)
        {
            var @event = await _eventRepo
                .GetAll()
                .Include(e => e.Registrations)
                .Where(e => e.Id == input.Id)
                .FirstOrDefaultAsync();

            if (@event == null)
            {
                throw new UserFriendlyException("Could not found the event, maybe it's deleted.");
            }

            return ObjectMapper.Map<EventDetailOutput>(@event);
        }

        public async Task Create(CreateEventInput input)
        {
            var @event = Event.Create(AbpSession.GetTenantId(), input.Title, input.Date, input.Description,
                input.NaxRegistrationCount);

            await _eventManager.CreateAsync(@event);
        }

        public async Task Cancel(EntityRequestInput<Guid> input)
        {
            var @event = await _eventManager.GetAsync(input.Id);
            _eventManager.Cancel(@event);
        }

        public async Task<EventRegisterOutput> Register(EntityRequestInput<Guid> input)
        {
            var registration = await RegisterAndSaveAsync(
                await _eventManager.GetAsync(input.Id),
                await GetCurrentUserAsync()
                );

            return new EventRegisterOutput
            {
                RegistrationId = registration.Id
            };

        }

        private async Task<EventRegistration> RegisterAndSaveAsync(Event @event, User user)
        {
            var registration = await _eventManager.RegisterAsync(@event, user);
            await CurrentUnitOfWork.SaveChangesAsync();
            return registration;
        }
    }

    /*application service does not implement domain logic itself. It just uses entities and domain services (EventManager) to perform the use cases.*/


}