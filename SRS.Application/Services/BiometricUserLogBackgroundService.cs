using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SRS.Application.Interfaces.Repository;
using SRS.Application.Interfaces.Service;

namespace SRS.Application.Services
{
    public class BiometricUserLogBackgroundService : BackgroundService
    {
        private readonly IBiometricUserLogQueue _queue;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<BiometricUserLogBackgroundService> _logger;

        public BiometricUserLogBackgroundService(IBiometricUserLogQueue queue, IServiceScopeFactory scopeFactory, ILogger<BiometricUserLogBackgroundService> logger)
        {
            _queue = queue;
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var model = await _queue.DequeueAsync(stoppingToken);

                    // Wait exactly 2 minutes
                    await Task.Delay(TimeSpan.FromMinutes(2), stoppingToken);

                    using var scope = _scopeFactory.CreateScope();
                    var repository = scope.ServiceProvider.GetRequiredService<IBiometricUserLogRepository>();
                    await repository.InsertIntoDevAsync(model, stoppingToken);

                    _logger.LogInformation("Biometric record {IndRegID} inserted", model.IndRegID);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while inserting biometric log");
                }
            }
        }
    }
}