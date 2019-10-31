using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace pricecheckingtoolapi.Services
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private readonly ILogger<TimedHostedService> _logger;
        private Timer _timer;
        private Task executeFetch;

        public TimedHostedService(ILogger<TimedHostedService> logger)
        {
            _logger = logger;
        }
        
        private void ExecuteTask(object state)
        {
            executionCount++;

            _logger.LogInformation(
                "is working. Count: {Count}", executionCount);

            if (executeFetch == null)
                executeFetch = ExecuteFetch();
            else if (executeFetch.IsCompleted)
                executeFetch = ExecuteFetch();
            else
                executeFetch.Wait();

        }

        private async Task ExecuteFetch()
        {
            string link = $"https://poe.ninja/api/data/itemoverview?league=Blight&type=Currency";
            HttpClient httpClient = new HttpClient();

            try
            {
                var responseString = await httpClient.GetStringAsync(link);
                _logger.LogInformation(responseString);
            }
            catch (HttpRequestException e)
            {
                throw e;
            }
            finally
            {
                httpClient.Dispose();
            }
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            _timer = new Timer(ExecuteTask, null, TimeSpan.Zero,
                TimeSpan.FromMinutes(30));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
    }
}
