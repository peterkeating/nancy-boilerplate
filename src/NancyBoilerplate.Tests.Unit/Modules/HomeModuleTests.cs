using Nancy;
using Nancy.Testing;
using NancyBoilerplate.Modules;
using NUnit.Framework;
using System;

namespace NancyBoilerplate.Tests.Unit.Modules
{
    public class HomeModuleTests : ModuleTestBase<HomeModule>
    {
        [SetUp]
        public void SetUp()
        {
            Configure();
        }

        [Test]
        public void Index_RespondsOK()
        {
            Get("/");
            Assert.That(Response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
