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
    public class CustomRootPathProvider : IRootPathProvider
    {
        public string GetRootPath()
        {
            return Path.GetDirectoryName(typeof(Bootstrapper).Assembly.Location);
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
                with.RootPathProvider<CustomRootPathProvider>();
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

        /// <summary>
        /// Sets up the mock response that would be handle in the real application by
        /// Razor, however
        /// </summary>
        /// <param name="path"></param>
        /// <param name="htmlResponse"></param>
        public void SetView(string path, HtmlResponse htmlResponse)
        {
            AutoMoqer.GetMock<IViewFactory>().Setup(v => v.RenderView(path, It.IsAny<object>(), It.IsAny<ViewLocationContext>()))
                       .Returns(htmlResponse);
        }
    }
}
