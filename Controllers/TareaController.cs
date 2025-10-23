using API_Buenas_practicas.Models;
using API_Buenas_practicas.Models.Dtos;
using API_Buenas_practicas.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Buenas_practicas.Controllers
{
    /// <summary>
    /// Controlador de Tarea
    /// </summary>
    /// <remarks>Aqui se declararan los endpoints para la API REST</remarks>
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {
        private readonly ITareaRepository _tareaRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor del controlador, recibe el repositorio de Tarea y el Mappeador de entidad
        /// </summary>
        /// <param name="tareaRepository"></param>
        /// <param name="mapper"></param>
        public TareaController(ITareaRepository tareaRepository, IMapper mapper)
        {
            _tareaRepository = tareaRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Endpoint para obtener todos los registros de la entidad 'Tareas'
        /// </summary>
        /// <remarks>Ejecuta la accion para obtener los registros de la DB y
        /// los mapea para mostrarlos por el Dto</remarks>
        /// <returns>TareaDto</returns>
        [HttpGet("AllTareas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTareas()
        {
            try
            {
                var accion = await _tareaRepository.GetAllTareas();
                var registros = _mapper.Map<IEnumerable<TareaDto>>(accion);
                return Ok(registros);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Endpoint para crear un nuevo registro de 'Tarea'
        /// </summary>
        /// <param name="crearTareaDto"></param>
        /// <remarks>Se ocupara CrearTareaDto para unicamente pedir los campos visibles al cliente,
        /// verificara si el from body no esta nulo, que no se repita un nombre existente</remarks>
        /// <returns>Cuerpo Entidad Tarea</returns>
        [HttpPost("NewTarea")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostTarea([FromBody] CrearTareaDto crearTareaDto)
        {
            try
            {
                if(crearTareaDto == null)
                {
                    return BadRequest();
                }
                bool existe = await _tareaRepository.ExistsTarea(crearTareaDto.Titulo);
                if (existe)
                {
                    ModelState.AddModelError("CustomError", $"El nombre \"{crearTareaDto.Titulo}\" ya existe");
                    return BadRequest(ModelState);
                }
                //Crear tarea
                var newRegistro = _mapper.Map<Tarea>(crearTareaDto);
                var registrarDb = await _tareaRepository.PostNewTarea(newRegistro);
                //Verificar, si no se creo, retornar un error del servidor
                bool seCreo = await _tareaRepository.ExistsTarea(crearTareaDto.Titulo);
                if (!seCreo)
                {
                    ModelState.AddModelError("CustomError", $"Ocurrio un error al registrar la tarea :c");
                    return BadRequest(ModelState);
                }
                return Ok(registrarDb);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Endpoint para obtener el registro de un Tarea con el Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Cuerpo entidad Tarea</returns>
        [HttpGet("TareaById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTareaById(int id)
        {
            try
            {
                bool existe = await _tareaRepository.ExistsTarea(id);
                if (!existe)
                {
                    ModelState.AddModelError("CustomError", $"El id {id} no existe!");
                    return NotFound(ModelState);
                }

                var accion = await _tareaRepository.GetTareaById(id);
                var registro = _mapper.Map<Tarea>(accion);
                return Ok(registro);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Endpoint Modificar el Titulo y Descripcion de 'Tarea'
        /// </summary>
        /// <param name="id"></param>
        /// <param name="actualizarTareaDto"></param>
        /// <returns></returns>
        [HttpPut("ModifyTarea/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutTarea(int id, [FromBody] ActualizarTareaDto actualizarTareaDto)
        {
            try
            {
                bool existeId = await _tareaRepository.ExistsTarea(id);
                if (!existeId)
                {
                    ModelState.AddModelError("CustomError", $"El id {id} no existe!");
                    return NotFound(ModelState);
                }
                bool existeNombre = await _tareaRepository.ExistsTarea(actualizarTareaDto.Titulo);
                if (existeNombre)
                {
                    ModelState.AddModelError("CustomError", 
                        $"El nombre '{actualizarTareaDto.Titulo}' ya se encuentra en uso, escoge otro!");
                    return BadRequest(ModelState);
                }
                //Modificar registro existente, ingresando los nuevos parametros
                var registro = await _tareaRepository.GetTareaById(id);
                _mapper.Map(actualizarTareaDto, registro);
                //Subirlo a la DB
                await _tareaRepository.PutTarea(registro);
                bool seActualizo = await _tareaRepository.ExistsTarea(actualizarTareaDto.Titulo);
                if (!seActualizo)
                {
                    ModelState.AddModelError("CustomError",
                        $"Hubo un error al actualizar en la DB :c");
                    return BadRequest(ModelState);
                }
                return Ok(registro);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Endpoint para marcar 'Tarea' como terminada
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("CompleteTarea/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PatchTarea(int id)
        {
            try
            {
                bool existeId = await _tareaRepository.ExistsTarea(id);
                if (!existeId)
                {
                    ModelState.AddModelError("CustomError", $"El id {id} no existe!");
                    return NotFound(ModelState);
                }
                bool accion = await _tareaRepository.PatchTerminasteLaTarea(id);
                if (!accion)
                {
                    ModelState.AddModelError("CustomError",
                        $"Hubo un error al actualizar en la DB :c");
                    return BadRequest(ModelState);
                }

                return Ok(accion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Endpoint para eliminar registro de 'Tarea'
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteTarea/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTarea(int id)
        {
            try
            {
                bool existeId = await _tareaRepository.ExistsTarea(id);
                if (!existeId)
                {
                    ModelState.AddModelError("CustomError", $"El id {id} no existe!");
                    return NotFound(ModelState);
                }
                var registro = _tareaRepository.GetTareaById(id);
                if (registro == null)
                {
                    ModelState.AddModelError("CustomError", $"No se encontroco Tarea!");
                    return NotFound(ModelState);
                }
                bool accion = await _tareaRepository.DeleteTarea(id);
                if (!accion)
                {
                    ModelState.AddModelError("CustomError", $"Algo salio mal!");
                    return NotFound(ModelState);
                }
                return Ok(accion);

            }
            catch(Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
    }
}
