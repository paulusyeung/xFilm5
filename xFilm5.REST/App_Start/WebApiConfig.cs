using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace xFilm5.REST
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            #region Web API configuration and services
            // 開啟 Authorization，暫時用 Basic Authentication
            config.Filters.Add(new AuthorizeAttribute());
            #endregion

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
