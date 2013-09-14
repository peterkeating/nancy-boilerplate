using Nancy;

namespace NancyBoilerplate.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ =>
            {
                return View["Home/Index"];
            };
        }
    }
}