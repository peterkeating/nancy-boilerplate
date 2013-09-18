using NancyBoilerplate.Core;
using NUnit.Framework;
using System;
using System.Configuration;

namespace NancyBoilerplate.Tests.Integration.Core
{
    public class ConfigTests
    {
        [Test]
        public void ConnectionString_ReturnsExpectedForEnvironmentMachineName()
        {
            Assert.That(Config.ConnectionString, Is.EqualTo(ConfigurationManager.ConnectionStrings[Environment.MachineName].ConnectionString));
        }
    }
}
