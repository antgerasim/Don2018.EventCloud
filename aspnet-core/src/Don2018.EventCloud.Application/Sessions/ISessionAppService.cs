using System.Threading.Tasks;
using Abp.Application.Services;
using Don2018.EventCloud.Sessions.Dto;

namespace Don2018.EventCloud.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
