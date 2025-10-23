namespace API_Buenas_practicas.Models.Dtos
{
    /// <summary>
    /// Dto Crear Tarea
    /// </summary>
    /// <remarks>Se le pediran los atributos al cliente, ocultando el Id</remarks>
    public class CrearTareaDto
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
