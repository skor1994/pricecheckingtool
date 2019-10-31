using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using pricecheckingtoolapi.Gateways;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace pricecheckingtoolapi.Services
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private readonly ILogger<TimedHostedService> _logger;
        private Timer _timer;
        private Task executeFetch;
        private HttpGateway httpGateway = new HttpGateway();

        public TimedHostedService(ILogger<TimedHostedService> logger)
        {
            _logger = logger;
        }
        
        private void ExecuteTask(object state)
        {
            if (executeFetch == null)
                executeFetch = ExecuteFetch();
            else if (executeFetch.IsCompleted)
                executeFetch = ExecuteFetch();
            else
                executeFetch.Wait();

        }

        private async Task ExecuteFetch()
        {
            List<Task> tasks = new List<Task>();

            _logger.LogInformation("Fetches Running.");

            List<string> urls = new List<string>{ $"https://poe.ninja/api/data/itemoverview?league=Blight&type=Currency", "https://poe.ninja/api/data/currencyoverview?league=Blight&type=Fragment",
                        "https://poe.ninja/api/data/itemoverview?league=Blight&type=Oil", "https://poe.ninja/api/data/itemoverview?league=Blight&type=Incubator",
                        "https://poe.ninja/api/data/itemoverview?league=Blight&type=Scarab", "https://poe.ninja/api/data/itemoverview?league=Blight&type=Fossil",
                        "https://poe.ninja/api/data/itemoverview?league=Blight&type=Resonator", "	https://poe.ninja/api/data/itemoverview?league=Blight&type=Essence",
                        "https://poe.ninja/api/data/itemoverview?league=Blight&type=DivinationCard", "https://poe.ninja/api/data/itemoverview?league=Blight&type=Prophecy",
                        "https://poe.ninja/api/data/itemoverview?league=Blight&type=SkillGem", "https://poe.ninja/api/data/itemoverview?league=Blight&type=BaseType",
                        "https://poe.ninja/api/data/itemoverview?league=Blight&type=UniqueMap", "https://poe.ninja/api/data/itemoverview?league=Blight&type=Map",
                        "https://poe.ninja/api/data/itemoverview?league=Blight&type=UniqueJewel", "	https://poe.ninja/api/data/itemoverview?league=Blight&type=UniqueFlask",
                        "https://poe.ninja/api/data/itemoverview?league=Blight&type=UniqueWeapon","	https://poe.ninja/api/data/itemoverview?league=Blight&type=UniqueArmour",
                        "https://poe.ninja/api/data/itemoverview?league=Blight&type=UniqueAccessory", "	https://poe.ninja/api/data/itemoverview?league=Blight&type=Beast"
            };
                
            foreach (var url in urls)
            {
                tasks.Add(Task.Run(async () => await httpGateway.Get(url)));
            }

            Task task = Task.WhenAll(tasks);
            
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _logger.LogInformation("Fetches Done.");
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
