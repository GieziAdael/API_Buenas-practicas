using API_Buenas_practicas.Models;
using API_Buenas_practicas.Models.Dtos;

namespace API_Buenas_practicas.Repositories.IRepositories
{
    /// <summary>
    /// Interfaz del repositorio de tareas
    /// </summary>
    public interface ITareaRepository
    {
        /// <summary>
        /// Obtener una lista de todas las tareas de la Db
        /// </summary>
        /// <returns>Entidad Tarea</returns>
        Task<IEnumerable<Tarea>> GetAllTareas();
        
        /// <summary>
        /// Consultar un registro por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Entidad Tarea</returns>
        Task<Tarea> GetTareaById(int id);
        /// <summary>
        /// Verificar si existe la tarea por nombre
        /// </summary>
        /// <param name="name"></param>
        /// <remarks>Esto se usara para no permitir que existan registros con nombre duplicados
        /// </remarks>
        /// <returns>bool</returns>
        Task<bool> ExistsTarea(string name);
        /// <summary>
        /// Verifica si existe el registro por Id
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>Un paso de verificacion antes de Acualizar o eliminar una tarea 
        /// en caso de que no exista</remarks>
        /// <returns></returns>
        Task<bool> ExistsTarea(int id);
        /// <summary>
        /// Crear un nuevo registro de la entidad 'Tarea'
        /// </summary>
        /// <returns>Entidad Tarea</returns>
        Task<bool> PostNewTarea(Tarea tarea);
        /// <summary>
        /// Modificar el Titulo y Descripcion
        /// </summary>
        /// <param name="tarea"></param>
        /// <returns>bool</returns>
        Task<bool> PutTarea(Tarea tarea);
        /// <summary>
        /// Actualizar el estado de 'Completado'
        /// </summary>
        /// <param name="tarea"></param>
        /// <returns>bool</returns>
        Task<bool> PatchTerminasteLaTarea(int id);
        /// <summary>
        /// Eliminar registro
        /// </summary>
        /// <param name="tarea"></param>
        /// <returns>bool</returns>
        Task<bool> DeleteTarea(int id);
    }
}
