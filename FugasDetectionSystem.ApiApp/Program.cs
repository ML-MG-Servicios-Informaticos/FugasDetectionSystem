
using FugasDetectionSystem.Domain.Interfaces;
using FugasDetectionSystem.Domain.Repositories;
using FugasDetectionSystem.Infrastructure.data;

namespace FugasDetectionSystem.ApiApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Registro de la cadena de conexión y repositorios
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            if (connectionString == null)
            {
                throw new ArgumentNullException(nameof(connectionString), "La cadena de conexión no puede ser nula. Asegúrate de configurarla en appsettings.json o en las variables de entorno.");
            }

            // Registro de DatabaseSettings con la cadena de conexión.
            var databaseSettings = new DatabaseSettings(connectionString);
            builder.Services.AddSingleton<IDatabaseSettings>(databaseSettings);

            builder.Services.AddScoped<ITecnicoRepository, TecnicoRepository>();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
