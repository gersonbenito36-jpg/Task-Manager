
using System.ComponentModel.DataAnnotations;
public class EditarTareaDTO
{
    [Required (ErrorMessage = "El título no debe quedar vacio")]
    public string Titulo {get; set;}

    [Required (ErrorMessage = "La descripción no debe quedar vacia")]
    public string Descripcion {get; set;}

    public DateTime FechaLimite {get; set;}
    public Estado Estado {get; set;}
    public Prioridad Prioridad {get; set;}

    [Range(1, int.MaxValue, ErrorMessage = "La categoría no es válida")]
    public int CategoriaId {get; set;}
}