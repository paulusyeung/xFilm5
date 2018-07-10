using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Configuration;
using Hangfire.SqlServer;
using Hangfire;
using System.Web.Http;

[assembly: OwinStartup(typeof(xFilm5.Bot.Startup))]

namespace xFilm5.Bot
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            #region Initialize Hangfire
            // more control options
            var conn = ConfigurationManager.ConnectionStrings["x5db"].ConnectionString;
            var options = new SqlServerStorageOptions
            {
                SchemaName = "Hangfire.x5.Bot"
            };
            Hangfire.GlobalConfiguration.Configuration.UseSqlServerStorage(conn, options);

            // simple
            //GlobalConfiguration.Configuration.UseSqlServerStorage("SysDb");

            app.UseHangfireDashboard();
            app.UseHangfireServer();
            #endregion

            var config = new HttpConfiguration();

            SwaggerConfig.Register(config);

            WebApiConfig.Register(config);

            app.UseWebApi(config);
        }
    }
}
