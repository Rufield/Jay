using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sweeter.DataProviders;
using Sweeter.Services.ConnectionFactory;
using Sweeter.Services.DataProviders;
using Sweeter.Services.HashService;
using Sweeter.Services.LoggerService;

namespace Sweeter
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));
            services.AddTransient<IAccountDataProvider, AccountDataProvider>();
            services.AddTransient<IPostDataProvider, PostDataProvider>();
            services.AddTransient<ICommentDataProvider, CommentDataProvider>();
            services.AddTransient<ILikesToCommentsProvider, LikesToCommentsProvider>();
            services.AddTransient<ILikesToPostsProvider, LikesToPostsProvider>();
            services.AddTransient<IUnsubscribesDataProvider, UnsubscribesDataProvider>();
            services.AddSingleton<IHashService, HashService>();
            services.AddTransient<IConnectionFactory, ConnectionFactory>();
            services.AddTransient<ILogger, LoggerService>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
              .AddCookie(options => {
                  options.CookieName = "Current";
              });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
           /* loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();*/
            app.UseStaticFiles();
       
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
