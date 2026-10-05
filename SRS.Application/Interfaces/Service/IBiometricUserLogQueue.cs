using SRS.Application.DTOs.BiometricUserLog;

namespace SRS.Application.Interfaces.Service
{
    public interface IBiometricUserLogQueue
    {
        ValueTask QueueAsync(BiometricUserLogCreateDto model, CancellationToken cancellationToken = default);
        ValueTask<BiometricUserLogCreateDto> DequeueAsync(CancellationToken cancellationToken);
    }
}