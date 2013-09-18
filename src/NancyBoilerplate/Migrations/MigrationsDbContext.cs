using NancyBoilerplate.Core;
using System.Data.Entity;

namespace NancyBoilerplate.Migrations
{
    public class MigrationsDbContext : DbContext
    {
        public MigrationsDbContext()
            : base(Config.ConnectionString)
        {

        }
    }
}