using FugasDetectionSystem.Domain.Entities;
using System.Collections.Generic;

namespace FugasDetectionSystem.Domain.Interfaces
{
    public interface ITecnicoRepository
    {
        // Método para verificar si la tabla de técnicos existe.
        //bool TableExists();

        // Método para crear la tabla de técnicos si no existe.
        //void CreateTable();

        // Método para agregar un nuevo técnico a la base de datos.
        void AddTecnico(Tecnico tecnico);

        // Método para obtener una lista de todos los técnicos.
        List<Tecnico> GetTecnicos();

        // Método para obtener un único técnico por su identificador.
        Tecnico GetTecnico(int tecnicoId);

        // Método para actualizar la información de un técnico existente.
        void UpdateTecnico(Tecnico tecnico);

        // Método para eliminar un técnico de la base de datos por su identificador.
        void DeleteTecnico(int tecnicoId);
    }
}
