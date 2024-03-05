using Telegram.Bot.Types;
using Telegram.Bot;
using FugasDetectionSystem.Domain.Interfaces;
using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Types.Enums;

namespace FugasDetectionSystem.BotWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ITelegramBotService _telegramBotService;

        public Worker(ILogger<Worker> logger, ITelegramBotService telegramBotService)
        {
            _logger = logger;
            _telegramBotService = telegramBotService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _telegramBotService.StartReceiving(HandleUpdateAsync, HandleErrorAsync);

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }

            _telegramBotService.StopReceiving();
        }

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {

            //verifica el tipo de mensaje voz, texto, imagen y otros los otros no se tomaran en cuenta... no se lo que son... almacenarlos para estudiarlos en un log cuando lleguen..

            if (update.Message != null)
            {
                if (update.Message.Type == MessageType.Audio)
                {

                }
                if (update.Message.Type == MessageType.Animation)
                {

                }
                if (update.Message.Type == MessageType.ChannelCreated)
                {

                }
                if (update.Message.Type == MessageType.ChatMemberLeft)
                {

                }
                if (update.Message.Type == MessageType.ChatMembersAdded)
                {

                }
                if (update.Message.Type == MessageType.ChatPhotoChanged)
                {

                }
                if (update.Message.Type == MessageType.ChatPhotoDeleted)
                {

                }
                if (update.Message.Type == MessageType.ChatShared)
                {

                }
                if (update.Message.Type == MessageType.ChatTitleChanged)
                {

                }
                if (update.Message.Type == MessageType.Contact)
                {

                }
                if (update.Message.Type == MessageType.Dice)
                {

                }
                if (update.Message.Type == MessageType.Document)
                {

                }
                if (update.Message.Type == MessageType.Game)
                {

                }
                if (update.Message.Type == MessageType.Invoice)
                {

                }
                if (update.Message.Type == MessageType.Location)
                {

                }
                if (update.Message.Type == MessageType.Photo)
                {

                }
                if (update.Message.Type == MessageType.Video)
                {

                }
                if (update.Message.Type == MessageType.Voice)
                {

                }
                if (update.Message.Type == MessageType.WebAppData)
                {

                }
                if (update.Message.Type == MessageType.Text)
                {

                }

                // Extraer el chatId del mensaje recibido
                var chatId = update.Message.Chat.Id;

                // Preparar el texto de la respuesta
                string responseText = "(" + update.Message.Type.ToString() + ") Estamos trabajando en este servicio. Para más información visita okFugas.cl";

                // Enviar el mensaje de respuesta
                await botClient.SendTextMessageAsync(chatId, responseText, cancellationToken: cancellationToken);

            }


        }


        private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Aquí va tu lógica para manejar errores
            return Task.CompletedTask;
        }
    }
}
