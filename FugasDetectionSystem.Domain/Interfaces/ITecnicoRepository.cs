using FugasDetectionSystem.Domain.Entities;
using System.Collections.Generic;

namespace FugasDetectionSystem.Domain.Interfaces
{
    public interface ITecnicoRepository
    {
        void CreateTable(); // Método para crear la tabla si no existe
        void AddTecnico(Tecnico tecnico); // Método para añadir un nuevo técnico
        List<Tecnico> GetTecnicos(); // Método para obtener todos los técnicos
        Tecnico GetTecnico(int idTecnico); // Método para obtener un técnico por ID
        void UpdateTecnico(Tecnico tecnico); // Método para actualizar un técnico existente
        void DeleteTecnico(int idTecnico); // Método para eliminar un técnico por ID
        bool TableExists(); // Método para verificar si la tabla existe
    }
}
