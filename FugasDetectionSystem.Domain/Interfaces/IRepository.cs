using FugasDetectionSystem.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FugasDetectionSystem.Domain.Interfaces
{
    /// <summary>
    /// Interfaz para el repositorio genérico.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Método para agregar una nueva entidad a la base de datos.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task AddAsync(T entity);

        /// <summary>
        /// Método para obtener una lista de todas las entidades.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync();
        
        /// <summary>
        /// Método para obtener una única entidad por su identificador.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(int id);
        
        /// <summary>
        /// Método para actualizar la información de una entidad existente.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task UpdateAsync(T entity);
        
        /// <summary>
        /// Método para eliminar una entidad de la base de datos por su identificador.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}


