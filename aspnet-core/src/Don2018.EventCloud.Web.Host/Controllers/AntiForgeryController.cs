using Microsoft.AspNetCore.Antiforgery;
using Don2018.EventCloud.Controllers;

namespace Don2018.EventCloud.Web.Host.Controllers
{
    public class AntiForgeryController : EventCloudControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
