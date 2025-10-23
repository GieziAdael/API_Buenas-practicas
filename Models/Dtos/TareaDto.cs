using System.ComponentModel.DataAnnotations;

namespace API_Buenas_practicas.Models.Dtos
{
    /// <summary>
    /// Dto de la entidad Tarea
    /// </summary>
    /// <remarks>Se usara para mostrar todos los atributos en una lista</remarks>
    public class TareaDto
    {
        /// <summary>
        /// Id a mostrar
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Titulo a mostrar
        /// </summary>
        public string? Titulo { get; set; }
        /// <summary>
        /// Descripcion a mostrar
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Indicado booleano a mostrar
        /// </summary>
        public bool Completada { get; set; }
        /// <summary>
        /// Fecha de creacion a mostrar
        /// </summary>
        public DateTime FechaCreacion { get; set; }
    }
}
