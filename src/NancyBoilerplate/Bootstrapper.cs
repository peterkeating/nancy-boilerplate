using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using NancyBoilerplate.Core;
using NancyBoilerplate.Core.Domain;
using NancyBoilerplate.Core.Domain.Mappings;
using NancyBoilerplate.Migrations;
using NHibernate;
using System.Data.Entity.Migrations;

namespace NancyBoilerplate
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        /// <summary>
        /// Runs when the application first spins up.
        /// </summary>
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            DoMigrations();
        }

        /// <summary>
        /// Runs when the application first spins up and should be used to define
        /// dependencies that have application scope lifetimes, or be registered
        /// as multi-instance.
        /// </summary>
        /// <param name="container"></param>
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            container.Register<ISessionFactory>(CreateSessionFactory());
        }
        
        /// <summary>
        /// Runs per request and should be used to register dependencies that have
        /// a request scope lifetime.
        /// </summary>
        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);

            container.Register<ISession>(container.Resolve<ISessionFactory>().OpenSession());
        }

        /// <summary>
        /// Creates a NHibernate session factory configured to the database ready to create
        /// session objects used during a request to interact with the database.
        /// </summary>
        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently
                .Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(Config.ConnectionString))
                .Mappings(m => m.AutoMappings.Add(AutoMap.AssemblyOf<User>(new MappingConfig()).UseOverridesFromAssemblyOf<UserMapping>()))
                .BuildSessionFactory();
        }

        /// <summary>
        /// Performs database migrations which handle any updates to the database.
        /// </summary>
        private static void DoMigrations()
        {
            var settings = new MigrationsConfig();
            var migrator = new DbMigrator(settings);
            migrator.Update();
        }
    }
}