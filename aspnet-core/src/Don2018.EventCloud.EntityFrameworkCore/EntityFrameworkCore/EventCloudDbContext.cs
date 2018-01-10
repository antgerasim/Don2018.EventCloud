using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Don2018.EventCloud.Authorization.Roles;
using Don2018.EventCloud.Authorization.Users;
using Don2018.EventCloud.MultiTenancy;

namespace Don2018.EventCloud.EntityFrameworkCore
{
    public class EventCloudDbContext : AbpZeroDbContext<Tenant, Role, User, EventCloudDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public EventCloudDbContext(DbContextOptions<EventCloudDbContext> options)
            : base(options)
        {
        }
    }
}
