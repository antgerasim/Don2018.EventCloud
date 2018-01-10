using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace Don2018.EventCloud.Controllers
{
    public abstract class EventCloudControllerBase: AbpController
    {
        protected EventCloudControllerBase()
        {
            LocalizationSourceName = EventCloudConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
