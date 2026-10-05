using SRS.Application.DTOs.BiometricUserLog;

namespace SRS.Application.Interfaces.Repository
{
    public interface IBiometricUserLogRepository
    {
        Task InsertIntoSrsAsync(BiometricUserLogCreateDto model, CancellationToken cancellationToken = default);
        Task InsertIntoDevAsync(BiometricUserLogCreateDto model, CancellationToken cancellationToken = default);
    }
}