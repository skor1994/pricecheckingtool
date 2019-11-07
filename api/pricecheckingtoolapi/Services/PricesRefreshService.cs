using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using pricecheckingtoolapi.Db;
using pricecheckingtoolapi.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace pricecheckingtoolapi.Services
{
    public class PricesRefreshService : HostedService
    {
        private readonly PricesProvider _pricesProvider;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<PricesRefreshService> _logger;

        public PricesRefreshService(PricesProvider pricesProvider, ILogger<PricesRefreshService> logger, IServiceScopeFactory scopeFactory)
        {
            _pricesProvider = pricesProvider;
            _logger = logger;
            _scopeFactory = scopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation("Task awake, starting fetches");

                using (var scope = _scopeFactory.CreateScope())
                {
                    DatabaseContext databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();                  
                    await _pricesProvider.ExecuteFetch(databaseContext, cancellationToken);
                }

                _logger.LogInformation("Fetches Done, Task asleep");
                await Task.Delay(TimeSpan.FromMinutes(5), cancellationToken);
                
            }
        }
    }
}
