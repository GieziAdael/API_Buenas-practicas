🧩 Practica - API de Tareas
Objetivo: Implementar buenas practicas en la creacion de una API REST

Esta es una practica a nivel junior, una elaboracion de una API RESTful basica, 
implementando repositorios, AutoMapper y a su vez, integrando comenarios XML para generar Documentacion 
de Swagger.

------------------------------------------------------------
🚀 Caracteristicas Principales
------------------------------------------------------------
'''
I) CRUD completo de la entidad Tarea
II) Arquitectura modular con separacion de capas
III) Repositorio / IRepositorio de la entidad Tarea
IV) Integracion de AutoMapper
V) Documentacion automatica generada por Swagger
'''

------------------------------------------------------------
🧱 Estructura del proyecto
------------------------------------------------------------
'''
📁 API_Buenas-practicas
 ┣📁 Controllers
 ┃┗ TareaController.cs
 ┣📁 Mapping
 ┃┗ TareaProfile.cs
 ┣📁 Models
 ┃┣📁 Dtos
 ┃┃┣ ActualizarTareaDto.cs
 ┃┃┣ CrearTareaDto.cs
 ┃┃┗ TareaDto.cs
 ┃┗ Tarea.cs
 ┣📁 Repositories
 ┃┣📁 IRepositories
 ┃┃┗ ITareaRepository.cs
 ┃┗ TareaRepository.cs
 ┣ appsettings.json
 ┣ Dockerfile
 ┣ Program.cs
 ┗ README.md

'''

------------------------------------------------------------
🧩 Entidad principal: Tarea
------------------------------------------------------------
'''
public int Id --> [Primary Key]
public string Titulo --> [nvarchar(50), NOT NULL]
public string Descripcion --> [nvarchar(150)]
public bool Completada -- [True or False]
public DateTime FechaCreacion --> [DateTime.Now]
'''

------------------------------------------------------------
🧰 Tecnologías utilizadas
------------------------------------------------------------
'''
- .NET 8.0 / ASP.NET Core Web API
- Entity Framework Core
- C# 12
- Swagger
- SQL Server LocalDB
'''


------------------------------------------------------------
🔗 Endpoints de: TareaController.cs
------------------------------------------------------------
GET -> /api/Tarea/AllTareas -> Endpoint para obtener todos los registros de la entidad 'Tareas'

POST -> /api/Tarea/NewTarea -> Endpoint para crear un nuevo registro de 'Tarea'

GET -> /api/Tarea/TareaById/{id} -> Endpoint para obtener el registro de un Tarea con el Id

PUT -> /api/Tarea/ModifyTarea/{id} -> Endpoint Modificar el Titulo y Descripcion de 'Tarea'

PATCH -> /api/Tarea/CompleteTarea/{id} -> Endpoint para marcar 'Tarea' como terminada

DELETE -> /api/Tarea/DeleteTarea/{id} -> Endpoint para eliminar registro de 'Tarea'

------------------------------------------------------------
💾 Ejemplo de JSON (Crear Tarea)
------------------------------------------------------------
1) POST -> /api/Tarea/NewTarea
[FromBody]
{
  "titulo": "Filosofia",
  "descripcion": "Leer pagina 50, razonar el por que de las cosas, reflexionar en el baño y suicidarse"
}
2) 200 -> true

------------------------------------------------------------
💾 Ejemplo de JSON (Consultar Tareas)
------------------------------------------------------------
1) GET -> /api/Tarea/AllTareas
2) 200 ->
[
  {
    "id": 3,
    "titulo": "Filosofia",
    "descripcion": "Leer pagina 50, razonar el por que de las cosas, reflexionar en el baño y suicidarse",
    "completada": false,
    "fechaCreacion": "2025-10-23T16:45:29.6950356"
  }
]

------------------------------------------------------------
💾 Ejemplo de JSON (Consultar Tarea por Id)
------------------------------------------------------------
1) GET -> /api/Tarea/TareaById/{6}
2) 404:
{
  "id": {
    "rawValue": "6",
    "attemptedValue": "6",
    "errors": [],
    "validationState": 2,
    "isContainerNode": false,
    "children": null
  },
  "CustomError": {
    "rawValue": null,
    "attemptedValue": null,
    "errors": [
      {
        "exception": null,
        "errorMessage": "El id 6 no existe!"
      }
    ],
    "validationState": 1,
    "isContainerNode": false,
    "children": null
  }
}

3) GET -> /api/Tarea/TareaById/{3}
4) 200 ->
{
  "id": 3,
  "titulo": "Filosofia",
  "descripcion": "Leer pagina 50, razonar el por que de las cosas, reflexionar en el baño y suicidarse",
  "completada": false,
  "fechaCreacion": "2025-10-23T16:45:29.6950356"
}

------------------------------------------------------------
💾 Ejemplo de JSON (Modificar Tarea)
------------------------------------------------------------
1) PUT -> /api/Tarea/ModifyTarea/{3}
[FromBody]
{
  "titulo": "Filosofia",
  "descripcion": "Leer pagina 50, y hacer resumen"
}

2)400 -> (Aqui no se puede tener 2 tareas con el mismo Titulo)
{
  "id": {
    "rawValue": "3",
    "attemptedValue": "3",
    "errors": [],
    "validationState": 2,
    "isContainerNode": false,
    "children": null
  },
  "CustomError": {
    "rawValue": null,
    "attemptedValue": null,
    "errors": [
      {
        "exception": null,
        "errorMessage": "El nombre 'Filosofia' ya se encuentra en uso, escoge otro!"
      }
    ],
    "validationState": 1,
    "isContainerNode": false,
    "children": null
  }
}

3) PUT -> /api/Tarea/ModifyTarea/{3}
[FromBody]
{
  "titulo": "Libro de Filosofia",
  "descripcion": "Leer pagina 50, y hacer resumen"
}
4)200 ->
{
  "id": 3,
  "titulo": "Libro de Filosofia",
  "descripcion": "Leer pagina 50, y hacer resumen",
  "completada": false,
  "fechaCreacion": "2025-10-23T16:45:29.6950356"
}

------------------------------------------------------------
💾 Ejemplo de JSON (Marcar Tarea completada)
------------------------------------------------------------
1) PATCH -> /api/Tarea/CompleteTarea/{3}
2) 200 -> true

------------------------------------------------------------
💾 Ejemplo de JSON (Eliminar Tarea)
------------------------------------------------------------
1) DELETE -> /api/Tarea/DeleteTarea/{3}
2) 200 -> true


------------------------------------------------------------
🧑‍💻 Autor
------------------------------------------------------------
Desarrollador: GieziAdael
Rol: Backend Developer (.NET Junior)
Contacto: giezi.tlaxcoapan@gmail.com