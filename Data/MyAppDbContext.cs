using API_Buenas_practicas.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Buenas_practicas.Data
{
    /// <summary>
    /// Contexto de la aplicacion que se ocupara para la base de datos con el uso de las entidades
    /// </summary>
    public class MyAppDbContext : DbContext
    {
        /// <summary>
        /// Constructor del DbContext
        /// </summary>
        /// <remarks>Se ocupara para levantar el servicio e inyeccion de
        /// dependencias a Program.cs</remarks>
        /// <param name="options"></param>
        public MyAppDbContext(DbContextOptions options) : base(options)
        {
            
        }
        /// <summary>
        /// Agregar y hacer referencia a la entidad 'Tarea'
        /// </summary>
        public DbSet<Tarea> Tareas { get; set; }
    }
}
