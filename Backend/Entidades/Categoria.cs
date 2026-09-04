public class Categoria
{
    public int Id {get; set;}
    public string Nombre {get; set;}
    public Guid UsuarioId {get; set;}
    public ICollection <Tarea> Tareas {get; set;}

}