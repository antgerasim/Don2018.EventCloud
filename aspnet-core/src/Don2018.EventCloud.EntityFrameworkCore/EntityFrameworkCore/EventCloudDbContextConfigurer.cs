using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Don2018.EventCloud.EntityFrameworkCore
{
    public static class EventCloudDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<EventCloudDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<EventCloudDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
