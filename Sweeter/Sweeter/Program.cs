using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Sweeter
{
    public class Program
    {
        public static void Main(string[] args)
        {
			var host = BuildWebHost(args);
			.UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

			using (var scope = host.Services.CreateScope())
			{
			var services = scope.ServiceProvider;

			try
			{
			// Requires using RazorPagesMovie.Models;
			SeedData.Initialize(services);
			}
			catch (Exception ex)
			{
			var logger = services.GetRequiredService<ILogger<Program>>();
			logger.LogError(ex, "An error occurred seeding the DB.");
			}
		}

			host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
    WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>()
        .ConfigureAppConfiguration((hostContext, config) =>
        {
            // delete all default configuration providers
            config.Sources.Clear();
            config.AddJsonFile("myconfig.json", optional: true);
        })
        .Build();
    }
}
