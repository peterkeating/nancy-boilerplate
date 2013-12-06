using Nancy;
using Nancy.ErrorHandling;
using Nancy.ViewEngines;
using System;

namespace NancyBoilerplate.Core.Handlers
{
    public class _404Handler : DefaultViewRenderer, IStatusCodeHandler
    {
        public _404Handler(IViewFactory factory) : base(factory) { }

        public bool HandlesStatusCode(HttpStatusCode statusCode, NancyContext context)
        {
            return statusCode == HttpStatusCode.NotFound;
        }

        public void Handle(HttpStatusCode statusCode, NancyContext context)
        {
            var response = RenderView(context, "404");
            response.StatusCode = HttpStatusCode.NotFound;
            context.Response = response;
        }
    }
}