using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using pricecheckingtoolapi.Db;
using pricecheckingtoolapi.Gateways;
using System;
using System.Collections.Generic;
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
        private readonly IServiceScopeFactory _scopeFactory;

        public TimedHostedService(ILogger<TimedHostedService> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
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
            _logger.LogInformation("Fetches Running.");

            List<string> urlsCurrency = new List<string>{$"https://poe.ninja/api/data/itemoverview?league=Blight&type=Currency", "https://poe.ninja/api/data/currencyoverview?league=Blight&type=Fragment"};

            List<string> urlsItems = new List<string>{$"https://poe.ninja/api/data/itemoverview?league=Blight&type=Oil", "https://poe.ninja/api/data/itemoverview?league=Blight&type=Incubator",
                        "https://poe.ninja/api/data/itemoverview?league=Blight&type=Scarab", "https://poe.ninja/api/data/itemoverview?league=Blight&type=Fossil",
                        "https://poe.ninja/api/data/itemoverview?league=Blight&type=Resonator", "https://poe.ninja/api/data/itemoverview?league=Blight&type=Essence",
                        "https://poe.ninja/api/data/itemoverview?league=Blight&type=DivinationCard", "https://poe.ninja/api/data/itemoverview?league=Blight&type=Prophecy",
                        "https://poe.ninja/api/data/itemoverview?league=Blight&type=SkillGem", "https://poe.ninja/api/data/itemoverview?league=Blight&type=BaseType",
                        "https://poe.ninja/api/data/itemoverview?league=Blight&type=UniqueMap", "https://poe.ninja/api/data/itemoverview?league=Blight&type=Map",
                        "https://poe.ninja/api/data/itemoverview?league=Blight&type=UniqueJewel", "	https://poe.ninja/api/data/itemoverview?league=Blight&type=UniqueFlask",
                        "https://poe.ninja/api/data/itemoverview?league=Blight&type=UniqueWeapon","	https://poe.ninja/api/data/itemoverview?league=Blight&type=UniqueArmour",
                        "https://poe.ninja/api/data/itemoverview?league=Blight&type=UniqueAccessory", "	https://poe.ninja/api/data/itemoverview?league=Blight&type=Beast"
            };

            using (var scope = _scopeFactory.CreateScope())
            {
                var databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

                foreach (var url in urlsCurrency)
                {
                    await Task.Run(async () => await httpGateway.GetCurrenies(url, databaseContext));
                }
                foreach (var url in urlsItems)
                {
                    await Task.Run(async () => await httpGateway.GetItems(url, databaseContext));
                }
            }

            _logger.LogInformation("Fetches Done.");
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
