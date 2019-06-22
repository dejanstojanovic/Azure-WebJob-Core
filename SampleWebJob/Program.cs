using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace SampleWebJob
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IHost host = new HostBuilder()
             .ConfigureHostConfiguration(configHost =>
             {
                 configHost.SetBasePath(Directory.GetCurrentDirectory());
                 configHost.AddEnvironmentVariables(prefix: "ASPNETCORE_");
                 configHost.AddCommandLine(args);
             })
             .ConfigureAppConfiguration((hostContext, configApp) =>
             {
                 configApp.SetBasePath(Directory.GetCurrentDirectory());
                 configApp.AddEnvironmentVariables(prefix: "ASPNETCORE_");
                 configApp.AddJsonFile($"appsettings.json", true);
                 configApp.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", true);
                 configApp.AddCommandLine(args);
             })
            .ConfigureServices((hostContext, services) =>
            {
                services.AddLogging();
                services.AddHostedService<ApplicationHostService>();
            })
            .ConfigureLogging((hostContext, configLogging) =>
            {
                configLogging.AddSerilog(new LoggerConfiguration()
                          .ReadFrom.Configuration(hostContext.Configuration)
                          .CreateLogger());
                configLogging.AddConsole();
                configLogging.AddDebug();
            })
            .Build();

            try
            {
                await host.RunAsync();
            }
            catch (HostingStopException) {
                //Host terminated
            }
        }
    }
}
