namespace API_Buenas_practicas.Models.Dtos
{
    /// <summary>
    /// Dto Actualizar Tarea
    /// </summary>
    /// <remarks>Se utilizara para mostrar al cliente los atributos 'Titulo y Descripcion' 
    /// que se podran modificar</remarks>
    public class ActualizarTareaDto
    {
        /// <summary>
        /// Titulo a mostrar
        /// </summary>
        public string? Titulo { get; set; }
        /// <summary>
        /// Descripcion a mostrar
        /// </summary>
        public string? Descripcion { get; set; }
    }
}
