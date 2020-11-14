using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // everything in `using` method will be cleaned up after execution 
            // with CreateScope do (CMD + .)
            using (var scope = host.Services.CreateScope()) 
            {
                var services = scope.ServiceProvider; 
                // get database content and migrate db
                try 
                {
                    // DataContext (CMD + . use persistence)
                    var context = services.GetRequiredService<DataContext>();
                    // applies any pending migration for the context to the db
                    // and will create db if it does not exist 
                    // Migrate (CMD + . bring in entityframeworkcore)
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    // logout what happen in terminal window
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occured during migration"); 
                }
            } 
            host.Run(); // run application 
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
