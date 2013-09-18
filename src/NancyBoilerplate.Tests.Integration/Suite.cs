using NancyBoilerplate.Migrations;
using NUnit.Framework;
using System;
using System.Data.Entity.Migrations;

namespace NancyBoilerplate.Tests.Integration
{
    [SetUpFixture]
    public class Suite
    {
        private const string BEGINNING = "0";

        private DbMigrator _migrator;

        [SetUp]
        public void Setup()
        {
            // Tests that migrations are able to run to build & update the database.
            DoMigrations();
        }

        [TearDown]
        public void Teardown()
        {
            // Tests the Down methods in the migrations by reseting the database
            // to it's original state.
            _migrator.Update(BEGINNING);
        }

        /// <summary>
        /// Performs the database migration which handles updates to the database.
        /// </summary>
        private void DoMigrations()
        {
            var settings = new MigrationsConfig();
            _migrator = new DbMigrator(settings);
            _migrator.Update();
        }
    }
}
