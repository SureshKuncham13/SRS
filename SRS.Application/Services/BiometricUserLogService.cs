using SRS.Application.DTOs.BiometricUserLog;
using SRS.Application.Interfaces.Repository;
using SRS.Application.Interfaces.Service;

namespace SRS.Application.Services
{
    public class BiometricUserLogService : IBiometricUserLogService
    {
        private readonly IBiometricUserLogRepository _repository;
        private readonly IBiometricUserLogQueue _queue;

        public BiometricUserLogService(IBiometricUserLogRepository repository, IBiometricUserLogQueue queue)
        {
            _repository = repository;
            _queue = queue;
        }

        public async Task<bool> InsertAsync(BiometricUserLogCreateDto model, CancellationToken cancellationToken = default)
        {
            if (model == null)
                return false;

            await _repository.InsertIntoSrsAsync(model, cancellationToken);
            await _queue.QueueAsync(model, cancellationToken);

            return true;
        }
    }
}