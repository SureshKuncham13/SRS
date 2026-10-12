using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SRS.Application.Interfaces.Repository;

namespace SRS.Application.Services;

public sealed class BiometricUserLogBackgroundService : BackgroundService
{
    private static readonly TimeSpan SyncInterval = TimeSpan.FromMinutes(2);
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<BiometricUserLogBackgroundService> _logger;

    public BiometricUserLogBackgroundService(IServiceScopeFactory scopeFactory, ILogger<BiometricUserLogBackgroundService> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Biometric synchronization background service started. Interval: {Interval}.", SyncInterval);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<IBiometricUserLogSyncRepository>();

                _logger.LogInformation("Starting dbo.Sync_Biometric_User_Logs3 at {Time}.", DateTimeOffset.Now);

                var result = await repository.ExecuteSyncProcedureAsync(stoppingToken);

                if (result is null)
                {
                    _logger.LogWarning("The synchronization procedure completed without returning its summary row.");
                }
                else
                {
                    _logger.LogInformation("Biometric synchronization completed. Inserted={Inserted}, Updated={Updated}, Source={Source}, Destination={Destination}.", result.InsertedRecords, result.UpdatedRecords, result.SourceRecords, result.DestinationRecords);
                }
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Biometric synchronization failed. It will retry after {Interval}.", SyncInterval);
            }
            try
            {
                await Task.Delay(SyncInterval, stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
        }

        _logger.LogInformation("Biometric synchronization background service stopped.");
    }
}