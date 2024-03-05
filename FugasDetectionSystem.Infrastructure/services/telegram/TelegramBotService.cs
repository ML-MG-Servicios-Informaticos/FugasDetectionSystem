using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using FugasDetectionSystem.Domain.Interfaces;
using FugasDetectionSystem.Application.Handlers;

namespace FugasDetectionSystem.Infrastructure.services.telegram
{
    public class TelegramBotService : ITelegramBotService
    {
        private readonly TelegramBotClient _botClient;
        private readonly CancellationTokenSource _cts = new();

        public TelegramBotService(string token)
        {
            _botClient = new TelegramBotClient(token);
        }

        public void StartReceiving(UpdateHandler updateHandler, ErrorHandler errorHandler)
        {
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>() // Recibe todos los tipos de actualizaciones
            };

            _botClient.StartReceiving(
                updateHandler: (botClient, update, ct) => updateHandler.Invoke(botClient, update, ct),
                pollingErrorHandler: (botClient, exception, ct) => errorHandler.Invoke(botClient, exception, ct),
                receiverOptions: receiverOptions,
                cancellationToken: _cts.Token
            );

        }

        public void StopReceiving()
        {
            _cts.Cancel();
        }
    }
}
