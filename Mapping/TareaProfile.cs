using API_Buenas_practicas.Models;
using API_Buenas_practicas.Models.Dtos;
using AutoMapper;

namespace API_Buenas_practicas.Mapping
{
    /// <summary>
    /// Clase del perfil de Tarea
    /// </summary>
    public class TareaProfile : Profile
    {
        /// <summary>
        /// Configuracion del perfil Tarea para el mappeo de dicha entidad.
        /// </summary>
        /// <remarks>Se usara para facilitar el uso del desarrollo (EFCore ,LINQ, AutoMapper)</remarks>
        public TareaProfile()
        {
            CreateMap<Tarea, TareaDto>().ReverseMap();
            CreateMap<Tarea, CrearTareaDto>().ReverseMap();
            CreateMap<Tarea, ActualizarTareaDto>().ReverseMap();
        }
    }
}
