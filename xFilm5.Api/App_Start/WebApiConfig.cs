using Microsoft.Restier.Providers.EntityFramework;
using Microsoft.Restier.Publishers.OData;
using Microsoft.Restier.Publishers.OData.Batch;
//using Microsoft.Restier.Publishers.OData.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.OData.Extensions;
using xFilm5.Api.Models;

namespace xFilm5.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.EnableUnqualifiedNameCall(true);
            RegisterXFilm5Api(config, GlobalConfiguration.DefaultServer);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        public static void RegisterXFilm5Api(HttpConfiguration config, HttpServer server)
        {
            config.MapRestierRoute<EntityFrameworkApi<xFilm5ApiEdm>>(
                "xFilm5",
                "api/xFilm5",
                new RestierBatchHandler(server));
        }

    }
}
