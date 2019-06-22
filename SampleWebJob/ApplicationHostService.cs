using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SampleWebJob
{
    public class ApplicationHostService : IHostedService
    {
        readonly ILogger<ApplicationHostService> _logger;
        readonly IConfiguration _configuration;
        readonly IHostingEnvironment _hostingEnvironment;
        public ApplicationHostService(
            ILogger<ApplicationHostService> logger,
            IConfiguration configuration,
            IHostingEnvironment hostingEnvironment
            )
        {
            _logger = logger;
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            //Do something
            _logger.LogWarning("Hello from console application");

            //Throw exception to terminate the host
            throw new HostingStopException();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }
    }
}
