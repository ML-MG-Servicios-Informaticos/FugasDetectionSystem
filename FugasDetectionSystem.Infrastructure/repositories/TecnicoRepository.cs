using Dapper;
using FugasDetectionSystem.Domain.Entities;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace FugasDetectionSystem.Infrastructure.Repositories
{
    public class TecnicoRepository
    {
        private readonly string _connectionString;

        public TecnicoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task InsertTecnicosAsync(List<Tecnico> tecnicos)
        {
            var sb = new StringBuilder();
            var parameters = new DynamicParameters();

            sb.Append("DELETE FROM tecnicos;");

            for (var i = 0; i < tecnicos.Count; i++)
            {
                sb.Append(
                    $"INSERT INTO tecnicos (nombres, apellidos, email, telefono) VALUES (@nombres{i}, @apellidos{i}, @email{i}, @telefono{i});");

                parameters.Add($"@nombres{i}", tecnicos[i].Nombres);
                parameters.Add($"@apellidos{i}", tecnicos[i].Apellidos);
                parameters.Add($"@email{i}", tecnicos[i].Email);
                parameters.Add($"@telefono{i}", tecnicos[i].Telefono);
            }

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(sb.ToString(), parameters);
            }

            Console.WriteLine("Finished inserting tecnicos.");
        }

        public async Task<IEnumerable<Tecnico>> GetAllTecnicosAsync()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Aquí se llama a la función almacenada usando Dapper.
                var tecnicosList = await connection.QueryAsync<Tecnico>(
                    "SELECT * FROM get_all_tecnicos();",
                    commandType: CommandType.Text
                );

                return tecnicosList;
            }
        }

    }
}
