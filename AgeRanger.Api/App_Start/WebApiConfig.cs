using System.Web.Http;

namespace AgeRanger.Api
{
  using System.Web.Http.Cors;

  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {
      // Web API configuration and services

      // Web API routes
      config.MapHttpAttributeRoutes();
      var cors = new EnableCorsAttribute("http://localhost:54565", "*", "*");
      config.EnableCors(cors);
     
      config.Routes.MapHttpRoute(
          name: "DefaultApi",
          routeTemplate: "api/{controller}/{id}",
          defaults: new { id = RouteParameter.Optional }
      );
    }
  }
}
