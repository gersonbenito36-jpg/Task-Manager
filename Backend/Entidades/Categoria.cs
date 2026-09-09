using System.Text.Json.Serialization;
public class Categoria
{
    public int Id {get; set;}
    public string Nombre {get; set;}
    public Guid UsuarioId {get; set;}

    // Se ignora en la respuesta JSON para evitar un ciclo infinito Categoria -> Tareas -> Categoria -> Tareas...
    // La propiedad se sigue usando internamente por EF Core (ej. cascada de borrado), solo no viaja al cliente.

    [JsonIgnore] 
    public ICollection <Tarea> Tareas {get; set;}
    // asi mismo la clase categoria apunta a muchas tareas, 
}