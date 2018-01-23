using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Don2018.EventCloud.Domain.Events.Dtos;

namespace Don2018.EventCloud.Domain.Events
{
    public interface IEventAppService : IApplicationService
    {
        Task Cancel(EntityRequestInput<Guid> input);
        Task Create(CreateEventInput input);
        Task<EventDetailOutput> GetDetail(EntityDto<Guid> input);
        Task<ListResultDto<EventListDto>> GetEvents(GetEventsInpput input);
        Task<EventRegisterOutput> Register(EntityRequestInput<Guid> input);
    }
}