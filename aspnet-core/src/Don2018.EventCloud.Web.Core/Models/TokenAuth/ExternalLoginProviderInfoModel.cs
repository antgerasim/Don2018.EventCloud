using Abp.AutoMapper;
using Don2018.EventCloud.Authentication.External;

namespace Don2018.EventCloud.Models.TokenAuth
{
    [AutoMapFrom(typeof(ExternalLoginProviderInfo))]
    public class ExternalLoginProviderInfoModel
    {
        public string Name { get; set; }

        public string ClientId { get; set; }
    }
}
