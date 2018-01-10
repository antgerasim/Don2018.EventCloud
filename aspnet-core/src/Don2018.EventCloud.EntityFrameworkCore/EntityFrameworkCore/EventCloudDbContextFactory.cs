using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Don2018.EventCloud.Configuration;
using Don2018.EventCloud.Web;

namespace Don2018.EventCloud.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class EventCloudDbContextFactory : IDesignTimeDbContextFactory<EventCloudDbContext>
    {
        public EventCloudDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<EventCloudDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            EventCloudDbContextConfigurer.Configure(builder, configuration.GetConnectionString(EventCloudConsts.ConnectionStringName));

            return new EventCloudDbContext(builder.Options);
        }
    }
}
