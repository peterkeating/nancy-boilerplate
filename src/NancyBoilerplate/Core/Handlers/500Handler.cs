using Nancy;
using Nancy.ErrorHandling;
using Nancy.ViewEngines;
using System;

namespace NancyBoilerplate.Core.Handlers
{
    public class _500Handler : DefaultViewRenderer, IStatusCodeHandler
    {
        public _500Handler(IViewFactory factory) : base(factory) { }

        public bool HandlesStatusCode(HttpStatusCode statusCode, NancyContext context)
        {
            return statusCode == HttpStatusCode.InternalServerError;
        }

        public void Handle(HttpStatusCode statusCode, NancyContext context)
        {
            var response = RenderView(context, "500");
            response.StatusCode = HttpStatusCode.InternalServerError;
            context.Response = response;
        }
    }
}