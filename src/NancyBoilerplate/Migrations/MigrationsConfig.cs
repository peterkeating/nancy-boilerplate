using System.Data.Entity.Migrations;

namespace NancyBoilerplate.Migrations
{
    public class MigrationsConfig : DbMigrationsConfiguration<MigrationsDbContext>
    {
        public MigrationsConfig()
        {
            AutomaticMigrationsEnabled = false;
        }
    }
}