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
using Abp.UI;
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

            return new ListResultDto<EventListDto>(events.MapTo<List<EventListDto>>());
        }

        public async Task<EventDetailOutput> GetDetail(EntityDto<Guid> input)
        {
            var @event = await _eventRepo.GetAll()
                .Include(e => e.Registrations)
                .Where(e => e.Id == input.Id)
                .FirstOrDefaultAsync();

            if (@event == null)
            {
                throw new UserFriendlyException("Could not found the event, maybe it's deleted.");
            }

            return ObjectMapper.Map<EventDetailOutput>(@event);
        }
    }

    internal interface IEventAppService
    {

    }

    [AutoMapFrom(typeof(Event))]
    public class EventDetailOutput : FullAuditedEntityDto<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool IsCancelled { get; set; }
        public virtual int MaxRegistrationCount { get; protected set; }
        public int RegistrationsCount { get; set; }
        public ICollection<EventRegistrationDto>  Registrations { get; set; }
    }

    [AutoMapFrom(typeof(EventRegistration))]
    public class EventRegistrationDto : CreationAuditedEntityDto
    {
        public virtual Guid  EventId { get; protected set; }
        public virtual long UserId { get; protected set; }
        public virtual string UserName { get; protected set; }
        public virtual string UserSurname { get; protected set; }
    }

    public class GetEventsInpput
    {
        public bool IncludeCancelledEvents { get; set; }
    }

    [AutoMapFrom(typeof(Event))]
    public class EventListDto : FullAuditedEntityDto<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public bool IsCancelled { get; set; }

        public virtual int MaxRegistrationCount { get; protected set; }

        public int RegistrationsCount { get; set; }
    }
}