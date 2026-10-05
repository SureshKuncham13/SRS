using System.Threading.Channels;
using SRS.Application.DTOs.BiometricUserLog;
using SRS.Application.Interfaces.Service;

namespace SRS.Application.Services
{
    public class BiometricUserLogQueue : IBiometricUserLogQueue
    {
        private readonly Channel<BiometricUserLogCreateDto> _queue;

        public BiometricUserLogQueue()
        {
            _queue = Channel.CreateUnbounded<BiometricUserLogCreateDto>();
        }

        public async ValueTask QueueAsync(BiometricUserLogCreateDto model, CancellationToken cancellationToken = default)
        {
            await _queue.Writer.WriteAsync(model, cancellationToken);
        }

        public async ValueTask<BiometricUserLogCreateDto> DequeueAsync(CancellationToken cancellationToken)
        {
            return await _queue.Reader.ReadAsync(cancellationToken);
        }
    }
}