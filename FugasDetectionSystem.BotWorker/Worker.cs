using Telegram.Bot.Types;
using Telegram.Bot;
using FugasDetectionSystem.Domain.Interfaces;
using FugasDetectionSystem.Domain.Entities;
using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Types.Enums;
using FugasDetectionSystem.Infrastructure.Services.Telegram.Interfaces;
using FugasDetectionSystem.Application.Managers;

namespace FugasDetectionSystem.BotWorker
{
    public class Worker(ILogger<Worker> logger, ITelegramBotService telegramBotService, ITecnicoRepository tecnicoRepository) : BackgroundService
    {
        private readonly ILogger<Worker> _logger = logger;
        private readonly ITelegramBotService _telegramBotService = telegramBotService;
        private readonly ITecnicoRepository tecnicoRepository = tecnicoRepository;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // En alg�n lugar dentro de tu bot worker
            var manager = new TecnicoManager(tecnicoRepository);
            var result = await manager.GetAllTecnicosAsync();
            if (result.IsSuccess)
            {
                // Puedes trabajar con result.Value que ser� una List<Tecnico>
            }
            else
            {
                // Manejar el error, result.ErrorMessage tendr� la descripci�n del error
            }

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
                /*if (update.Message.Type == MessageType.Photo){}
                if (update.Message.Type == MessageType.Video){}
                if (update.Message.Type == MessageType.Voice){}
                */
                if (update.Message.Type == MessageType.Text){

                }

                // Extraer el chatId del mensaje recibido
                var chatId = update.Message.Chat.Id;

                // Preparar el texto de la respuesta
                string responseText = "(" + update.Message.Type.ToString() + ") Estamos trabajando en este servicio. Para m�s informaci�n visita okFugas.cl";

                // Enviar el mensaje de respuesta
                await botClient.SendTextMessageAsync(chatId, responseText, cancellationToken: cancellationToken);

            }


        }


        private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Aqu� va tu l�gica para manejar errores
            return Task.CompletedTask;
        }
    }
}
