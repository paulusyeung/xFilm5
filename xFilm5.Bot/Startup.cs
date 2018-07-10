using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Configuration;
using Hangfire.SqlServer;
using Hangfire;
using System.Web.Http;
using System.Web;
using Hangfire.Dashboard;
using System.Net;

[assembly: OwinStartup(typeof(xFilm5.Bot.Startup))]

namespace xFilm5.Bot
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            #region Initialize Hangfire

            #region database connection string and custom schema
            // simple
            //GlobalConfiguration.Configuration.UseSqlServerStorage("SysDb");

            // more control options
            var conn = ConfigurationManager.ConnectionStrings["x5db"].ConnectionString;
            var options = new SqlServerStorageOptions
            {
                SchemaName = "Hangfire.x5.Bot"
            };
            Hangfire.GlobalConfiguration.Configuration.UseSqlServerStorage(conn, options);
            #endregion

            #region Hangfire 祇容許 localhost，要用 MyAuthorizationFilter 搞
            // refer: http://docs.hangfire.io/en/latest/configuration/using-dashboard.html?highlight=authorization#configuring-authorization
            var dashOptions = new DashboardOptions {
                AppPath = VirtualPathUtility.ToAbsolute("~"),
                Authorization = new[]
                {
                    new MyAuthorizationFilter()
                }
            };
            app.UseHangfireDashboard("/hangfire", dashOptions);
            #endregion

            app.UseHangfireServer();
            #endregion

            var config = new HttpConfiguration();

            SwaggerConfig.Register(config);

            WebApiConfig.Register(config);

            app.UseWebApi(config);
        }

        public class MyAuthorizationFilter : IDashboardAuthorizationFilter
        {
            public bool Authorize(DashboardContext context)
            {
                // In case you need an OWIN context, use the next line, `OwinContext` class
                // is the part of the `Microsoft.Owin` package.
                var owinContext = new OwinContext(context.GetOwinEnvironment());
                
                // Allow all authenticated users to see the Dashboard (potentially dangerous).
                //return owinContext.Authentication.User.Identity.IsAuthenticated;
                //
                // allow anonymous
                return true;
            }
        }
    }
}
