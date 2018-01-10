using System.Threading.Tasks;
using Abp.Application.Services;
using Don2018.EventCloud.Authorization.Accounts.Dto;

namespace Don2018.EventCloud.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
