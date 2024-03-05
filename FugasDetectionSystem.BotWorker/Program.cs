using FugasDetectionSystem.Domain.Interfaces;
using FugasDetectionSystem.Infrastructure.services.telegram;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System;

namespace FugasDetectionSystem.BotWorker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            // Agregar configuración
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            // Configurar la inyección de dependencias
            builder.Services.AddSingleton<ITelegramBotService>(provider =>
            {
                // Asegúrate de que tu token de la API de Telegram esté en tu archivo appsettings.json
                var token = builder.Configuration["TelegramApiToken"];

                // Si el token es nulo, lanzar una excepción clara para indicar que falta la configuración
                if (string.IsNullOrEmpty(token))
                {
                    throw new ArgumentNullException(nameof(token), "El token de la API de Telegram no puede ser nulo o vacío. Asegúrate de configurarlo en appsettings.json o en las variables de entorno.");
                }

                return new TelegramBotService(token);
            });
            builder.Services.AddHostedService<Worker>();

            // Construir y ejecutar la aplicación
            var host = builder.Build();
            host.Run();
        }
    }
}
