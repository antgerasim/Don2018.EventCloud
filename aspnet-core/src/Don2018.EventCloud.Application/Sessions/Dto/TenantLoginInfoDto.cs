using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Don2018.EventCloud.MultiTenancy;

namespace Don2018.EventCloud.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}
