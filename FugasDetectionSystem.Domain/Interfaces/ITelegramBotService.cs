using FugasDetectionSystem.Application.Handlers;

namespace FugasDetectionSystem.Domain.Interfaces
{
    public interface ITelegramBotService
    {
        void StartReceiving(UpdateHandler updateHandler, ErrorHandler errorHandler);
        void StopReceiving();
    }
}
