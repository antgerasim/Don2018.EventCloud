using Abp.Authorization;
using Don2018.EventCloud.Authorization.Roles;
using Don2018.EventCloud.Authorization.Users;

namespace Don2018.EventCloud.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
