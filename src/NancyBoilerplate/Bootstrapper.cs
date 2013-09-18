using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using NancyBoilerplate.Migrations;
using System.Data.Entity.Migrations;

namespace NancyBoilerplate
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            DoMigrations();
        }

        /// <summary>
        /// Performs database migration which handles updates to the database.
        /// </summary>
        private static void DoMigrations()
        {
            var settings = new MigrationsConfig();
            var migrator = new DbMigrator(settings);
            migrator.Update();
        }
    }
}