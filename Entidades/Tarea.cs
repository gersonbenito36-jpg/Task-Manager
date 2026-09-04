public class Tarea
{
    public int TareaId {get; set;}
    public string Titulo {get; set;}
    public string Descripcion {get; set;}
    public DateTime FechaCreacion {get; set;}
    public DateTime FechaLimite {get; set;}
    public Estado Estado {get; set;}
    public Prioridad Prioridad {get; set;}
    public Guid UsuarioId {get; set;}
    public int CategoriaId {get; set;}
    public Categoria Categoria {get; set;}
}