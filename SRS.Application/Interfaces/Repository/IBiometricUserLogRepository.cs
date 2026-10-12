using SRS.Application.Models;
namespace SRS.Application.Interfaces.Repository;

public interface IBiometricUserLogSyncRepository
{
    Task<BiometricSyncResult?> ExecuteSyncProcedureAsync(CancellationToken cancellationToken = default);
}