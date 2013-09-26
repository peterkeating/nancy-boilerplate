using AutoMoq;
using Moq;
using Nancy;
using Nancy.Responses;
using Nancy.Testing;
using Nancy.ViewEngines;
using System;
using System.IO;

namespace NancyBoilerplate.Tests.Unit.Modules
{
    public class TestingRootPathProvider : IRootPathProvider
    {
        private static readonly string RootPath;

        static TestingRootPathProvider()
        {
            var directoryName = Path.GetDirectoryName(typeof(Bootstrapper).Assembly.CodeBase);

            if (directoryName != null)
            {
                var assemblyPath = directoryName.Replace(@"file:\", string.Empty);
                RootPath = Path.Combine(assemblyPath, "..", "..", "..", "NancyBoilerplate");
            }
        }

        public string GetRootPath()
        {
            return RootPath;
        }
    }

    public class ModuleTestBase<TModule> where TModule : NancyModule
    {
        protected AutoMoqer AutoMoqer;
        protected ConfigurableBootstrapper Bootstrapper;
        protected Browser Browser;
        protected BrowserResponse Response;

        protected void Configure()
        {
            AutoMoqer = new AutoMoqer();

            Bootstrapper = new ConfigurableBootstrapper(with =>
            {
                with.Module<TModule>();
                with.NancyEngine<NancyEngine>();
                with.RootPathProvider<TestingRootPathProvider>();
            });
        }

        /// <summary>
        /// Simulates a GET request to the path specified. The response from of the dummy
        /// request can be found inside the Response object.
        /// </summary>
        protected void Get(string path, Action<BrowserContext> browserContext = null)
        {
            Browser = new Browser(Bootstrapper);
            Response = Browser.Get(path, browserContext);
        }

        /// <summary>
        /// Simulates a POST request to the path specified. The response from of the dummy
        /// request can be found inside the Response object.
        /// </summary>
        protected void Post(string path, Action<BrowserContext> browserContext = null)
        {
            Browser = new Browser(Bootstrapper);
            Response = Browser.Post(path, browserContext);
        }
    }
}
