using API_Buenas_practicas.Data;
using API_Buenas_practicas.Models;
using API_Buenas_practicas.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace API_Buenas_practicas.Repositories
{
    /// <summary>
    /// Repositorio de la entidad Tarea
    /// </summary>
    /// <remarks>Aqui se definiran las acciones para la DB</remarks>
    public class TareaRepository : ITareaRepository
    {
        private readonly MyAppDbContext _context;

        /// <summary>
        /// Constructor de Tarea, para llamar el contexto de la Db
        /// </summary>
        /// <param name="context"></param>
        public TareaRepository(MyAppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtener una lista de todas las tareas de la Db
        /// </summary>
        /// <returns>Entidad Tarea</returns>
        public async Task<IEnumerable<Tarea>> GetAllTareas()
        {
            var listaRegistros = await _context.Tareas.ToListAsync();
            return listaRegistros;
        }
        
        /// <summary>
        /// Consultar un registro por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Entidad Tarea</returns>
        public async Task<Tarea> GetTareaById(int id)
        {
            var registro = await _context.Tareas.FirstOrDefaultAsync(d=>d.Id == id);
            return registro;
        }
        /// <summary>
        /// Verificar si existe la tarea por nombre
        /// </summary>
        /// <param name="name"></param>
        /// <remarks>Esto se usara para no permitir que existan registros con nombre duplicados
        /// </remarks>
        /// <returns>bool</returns>
        public async Task<bool> ExistsTarea(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }
            return await _context.Tareas.AnyAsync
                (d => d.Titulo.Trim().ToLower() == name.Trim().ToLower());
        }
        /// <summary>
        /// Verifica si existe el registro por Id
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>Un paso de verificacion antes de Acualizar o eliminar una tarea 
        /// en caso de que no exista</remarks>
        /// <returns></returns>
        public async Task<bool> ExistsTarea(int id)
        {
            if(id <= 0)
            {
                return false;
            }
            return await _context.Tareas.AnyAsync(d => d.Id == id);
        }
        /// <summary>
        /// Crear un nuevo registro de la entidad 'Tarea'
        /// </summary>
        /// <returns>Entidad Tarea</returns>
        public async Task<bool> PostNewTarea(Tarea tarea)
        {
            tarea.Completada = false;
            tarea.FechaCreacion = DateTime.Now;

            await _context.Tareas.AddAsync(tarea);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Modificar el Titulo y Descripcion
        /// </summary>
        /// <param name="tarea"></param>
        /// <returns>bool</returns>
        public async Task<bool> PutTarea(Tarea tarea)
        {
            _context.Tareas.Update(tarea);
            await _context.SaveChangesAsync();
            return true;
        }
        /// <summary>
        /// Actualizar el estado de 'Completado'
        /// </summary>
        /// <param name="tarea"></param>
        /// <returns>bool</returns>
        public async Task<bool> PatchTerminasteLaTarea(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);
            tarea.Completada = true;
            _context.Tareas.Update(tarea);
            await _context.SaveChangesAsync();
            return true;
        }
        /// <summary>
        /// Eliminar registro
        /// </summary>
        /// <param name="tarea"></param>
        /// <returns>bool</returns>
        public async Task<bool> DeleteTarea(int id)
        {
            var registro = await _context.Tareas.FindAsync(id);
            _context.Tareas.Remove(registro);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
