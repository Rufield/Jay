using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Sweeter
{
    public class Startup
    {
		public Startup(IConfiguration configuration)
		{
		Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc(o => o.Conventions.Add(new FeatureConvention()))
             .AddRazorOptions(options =>
             {
                // {0} - Action Name
                // {1} - Controller Name
                // {2} - Feature Name
                // Replace normal view location entirely
                options.ViewLocationFormats.Clear();
                 options.ViewLocationFormats.Add("/Features/{2}/{1}/{0}.cshtml");
                 options.ViewLocationFormats.Add("/Features/{2}/{0}.cshtml");
                 options.ViewLocationFormats.Add("/Features/Shared/{0}.cshtml");
                 options.ViewLocationExpanders.Add(new FeatureFoldersRazorViewEngine());
             });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
