using SRS.Application.DTOs.BiometricUserLog;

namespace SRS.Application.Interfaces.Service
{
    public interface IBiometricUserLogService
    {
        Task<bool> InsertAsync(BiometricUserLogCreateDto model, CancellationToken cancellationToken = default);
    }
}