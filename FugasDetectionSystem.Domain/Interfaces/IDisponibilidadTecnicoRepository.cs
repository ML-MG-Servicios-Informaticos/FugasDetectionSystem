using FugasDetectionSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FugasDetectionSystem.Domain.Interfaces
{
    public interface IDisponibilidadTecnicoRepository
    {
        // Método para agregar una nueva disponibilidad de técnico a la base de datos.
        void AddDisponibilidadTecnico(DisponibilidadTecnico disponibilidadTecnico);

        // Método para obtener una lista de todas las disponibilidades de técnicos.
        List<DisponibilidadTecnico> GetDisponibilidadesTecnico();

        // Método para obtener una única disponibilidad de técnico por su identificador.
        DisponibilidadTecnico GetDisponibilidadTecnico(int disponibilidadTecnicoId);

        // Método para actualizar la información de una disponibilidad de técnico existente.
        void UpdateDisponibilidadTecnico(DisponibilidadTecnico disponibilidadTecnico);

        // Método para eliminar una disponibilidad de técnico de la base de datos por su identificador.
        void DeleteDisponibilidadTecnico(int disponibilidadTecnicoId);
    }
}
