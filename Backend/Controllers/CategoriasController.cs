using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

[Route("api/[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly AppDbContext _context;
    public CategoriasController (AppDbContext context)
    {
        _context = context;
    }
    [Authorize]
    [HttpPost("crearCategoria")]
    public async Task<IActionResult> CrearCategoria ([FromBody] CrearCategoriaDTO dto)
    {
        var usuarioTexto = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(usuarioTexto == null)
        {
            return Unauthorized("No se puede crear una categoría, Usuario no autenticado");
        }   
        Guid userId = Guid.Parse(usuarioTexto);
        var nuevaCategoria = new Categoria
        {
            Nombre = dto.Nombre,
            UsuarioId = userId,

        };
        _context.Categorias.Add(nuevaCategoria);
        await _context.SaveChangesAsync();
        return Ok("Categoria creada con éxito..");
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> ObtenerCategoria ()
    {
        var usuarioTexto = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(usuarioTexto == null)
        {
            return Unauthorized("No se pueden obtener las categorias, Usuario no autenticado");
        }
        Guid userId = Guid.Parse(usuarioTexto);
        var obtenerCategoria = await _context.Categorias.Where(c =>c.UsuarioId == userId).ToListAsync();
        return Ok(obtenerCategoria);
    }

    [Authorize]
    [HttpPut("editarCategoria/{id}")]
    public async Task<IActionResult> EditarCategoria([FromRoute] int id, [FromBody] EditarCategoriaDTO dto)
    {
        var usuarioTexto = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(usuarioTexto == null)
        {
            return Unauthorized("Usuario no autentico, no se puede editar");
        }
        Guid usuarioId = Guid.Parse(usuarioTexto);

        var categoriaExistente = await _context.Categorias.FirstOrDefaultAsync(c=> c.Id == id && c.UsuarioId == usuarioId);
        if(categoriaExistente == null)
        {
            return StatusCode(403, "Esta categoría no existe o usuario no autenticado");
        }

        categoriaExistente.Nombre = dto.Nombre;
        await _context.SaveChangesAsync();
        return Ok("Categoria editada con exito");

    }

    [Authorize]
    [HttpDelete("eliminarCategoria/{id}")]
    public async Task<IActionResult> EliminarCategoria ([FromRoute] int id)
    {
        var usuarioTexto = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(usuarioTexto == null)
        {
            return Unauthorized("Error, no se puede eliminar, usuario no autenticado");
        }
        Guid usuarioId = Guid.Parse(usuarioTexto);
        var eliminarCategoria = await _context.Categorias.FirstOrDefaultAsync(cat => cat.Id == id && cat.UsuarioId == usuarioId);
        if(eliminarCategoria == null)
        {
            return StatusCode(403, "Categoria no encontrada o usuario no autenticado");
        }
         _context.Categorias.Remove(eliminarCategoria);
         await _context.SaveChangesAsync();
         return Ok("Categoria eliminada con éxito...");
    }
}