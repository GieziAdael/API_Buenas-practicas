using System.ComponentModel.DataAnnotations;

namespace API_Buenas_practicas.Models
{
    /// <summary>
    /// Entidad Tarea
    /// </summary>
    public class Tarea
    {
        /// <summary>
        /// Identificador unico de la entidad Tarea
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Titulo de la entidad
        /// </summary>
        [Required]
        [StringLength(50)]
        public string? Titulo { get; set; }
        /// <summary>
        /// Descripcion asociada a la entidad
        /// </summary>
        [StringLength(150)]
        public string? Descripcion { get; set; }
        /// <summary>
        /// Indicador booleano de tarea completada
        /// </summary>
        public bool Completada { get; set; }
        /// <summary>
        /// Fecha de creacion de la entidad
        /// </summary>
        public DateTime FechaCreacion { get; set; }
    }
}
