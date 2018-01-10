using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Don2018.EventCloud.Configuration;

namespace Don2018.EventCloud.Web.Host.Startup
{
    [DependsOn(
       typeof(EventCloudWebCoreModule))]
    public class EventCloudWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public EventCloudWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(EventCloudWebHostModule).GetAssembly());
        }
    }
}
