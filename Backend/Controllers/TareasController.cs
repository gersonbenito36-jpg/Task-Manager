using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

[Route("api/[controller]")]
[ApiController]
public class TareasController : ControllerBase
{
    private readonly AppDbContext _context;
    public TareasController(AppDbContext context)
    {
        _context = context;
    }
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> ObtenerTareas([FromQuery] Estado? estado, [FromQuery] int? categoriaId)
    {
        var usuarioTexto = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;// sacamos el ID del usuario desde el TOKEN, que ya validó UseAuthentication
        if(usuarioTexto == null)
        {
            return Unauthorized ("No se ha encontrado un usuario autenticado"); // si no hay un usuario autenticado, lanzará un error
        }
        Guid usuarioId = Guid.Parse(usuarioTexto);// convertimos el id a un guid nuevamente a traves de un parseo
        var query = _context.Tareas.Include(tar => tar.Categoria).Where(tar => tar.UsuarioId == usuarioId);  // se obtiene la lista de tareas del usuario autenticado, filtrando por el ID del usuario

        if(estado != null)
        {
            query = query.Where(tar => tar.Estado == estado);
        }
        if(categoriaId != null)
        {
            query = query.Where(tar => tar.CategoriaId == categoriaId );
        }
        var tareas = await query.ToListAsync();
        return Ok(tareas);
    }

    [Authorize]
    [HttpPost("crearTarea")]
    public async Task<IActionResult> CrearTarea ([FromBody] CrearTareaDTO dto)
    {
        var usuarioTexto = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // sacamos el ID del usuario desde el TOKEN, que ya validó UseAuthentication
        if(usuarioTexto == null)
        {
            return Unauthorized("No se ha encontrado un usuario autenticado"); // si no hay un usuario autenticado, lanzará un error
        }
        Guid usuarioId = Guid.Parse(usuarioTexto); // convertimos el id a un guid nuevamente para poder guardarlo en la base de datos, ya que el campo UsuarioId es de tipo Guid
        if(dto.FechaLimite.Date <DateTime.UtcNow.Date)
        {
            return BadRequest("La fecha no puede ser en el pasado");
        }


        var nuevaTarea = new Tarea
        {
            
            Titulo = dto.Titulo,
            Descripcion = dto.Descripcion,
            FechaCreacion = DateTime.UtcNow,
            FechaLimite = DateTime.SpecifyKind(dto.FechaLimite, DateTimeKind.Utc),
            Estado = dto.Estado,
            Prioridad = dto.Prioridad,
            CategoriaId = dto.CategoriaId,
            UsuarioId = usuarioId
        };
        _context.Tareas.Add(nuevaTarea);
        await _context.SaveChangesAsync();
        return Ok("Tarea guardada con éxito");
    }   

    [Authorize]
    [HttpPut("editarTarea/{id}")] // se agrega la ruta para que el id de la tarea a editar se pase como parámetro en la URL
    public async Task<IActionResult> EditarTarea([FromRoute] int id, [FromBody] EditarTareaDTO dto) // acá se agrega el parámetro id para que se pueda identificar la tarea a editar, y se agrega el DTO para que se puedan recibir los datos a editar
    {
        var usuarioTexto = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(usuarioTexto == null) // acá si el usuario no está autenticado, se devuelve un error 401 Unauthorized
        {
            return Unauthorized("No se puede editar esta tarea, usuario no autenticado");
        }
        Guid usuarioId = Guid.Parse(usuarioTexto);

        if(dto.FechaLimite.Date < DateTime.UtcNow.Date)
        {
            return BadRequest("La fecha limite no puede ser actualizada en el pasado");
        }

        // acá tareExistente es la tarea que se quiere editar, y se busca en la base de datos por el id de la tarea y el id del usuario, para asegurarse de que el usuario autenticado es el dueño de la tarea
        var tareaExistente = await _context.Tareas.FirstOrDefaultAsync(t => t.TareaId == id && t.UsuarioId == usuarioId); // t.TareaId es el id de la tarea que se quiere editar, y t.UsuarioId es el id del usuario que está autenticado, para asegurarse de que el usuario autenticado es el dueño de la tarea.
        // lo que hace FirstOrDefaultAsync es que busca la primera tarea que cumpla con la condición, y si no encuentra ninguna, devuelve null. Por eso se hace la validación de que si tareaExistente es null, se devuelve un error 401 Unauthorized, porque significa que la tarea no existe o el usuario no es el dueño de la tarea.
        if(tareaExistente == null)
        {
            return StatusCode (403, "Esta tarea no existe o usuario no ha sido identificado");
        }

           tareaExistente.Titulo = dto.Titulo;
           tareaExistente.Descripcion = dto.Descripcion;
           tareaExistente.FechaLimite = DateTime.SpecifyKind(dto.FechaLimite, DateTimeKind.Utc);
           tareaExistente.Estado = dto.Estado;
           tareaExistente.Prioridad = dto.Prioridad;
           tareaExistente.CategoriaId = dto.CategoriaId;

        //_context.Tareas.Update(tareaExistente); no necesitamos guardar la tarea existente, ya que el contexto de EF Core rastrea los cambios automáticamente y los aplicará al guardar los cambios. Por lo tanto, no es necesario llamar a Update() explícitamente.
        await _context.SaveChangesAsync(); //unicamente se necesita llamar a SaveChangesAsync() para guardar los cambios en la base de datos.
        return Ok("Tarea editada con éxito");
    }

    [Authorize]
    [HttpDelete("eliminarTarea/{id}")]    
    public async Task<IActionResult> EliminarTarea ([FromRoute] int id)
    {
        var usuarioTexto = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(usuarioTexto == null)
        {
            return Unauthorized("Usuano no autenticado");
        }
        Guid usuarioId = Guid.Parse(usuarioTexto);

        var eliminarTarea = await _context.Tareas.FirstOrDefaultAsync(t => t.TareaId == id && t.UsuarioId == usuarioId);
        if(eliminarTarea == null)
        {
            return StatusCode(403, "Error, tarea no encontrada o usuario no autenticado");
        }
        _context.Tareas.Remove(eliminarTarea);
        await _context.SaveChangesAsync();
        return Ok("Tarea eliminada con éxito");
    }
}