using System.ComponentModel.DataAnnotations;
public class CrearTareaDTO
{
    [Required(ErrorMessage = "El título es obligatorio")]
    public string Titulo {get; set;}

    [Required(ErrorMessage = "La descripción es obligatoria")]
    public string Descripcion {get; set;}


    public DateTime FechaLimite {get; set;}
    public Estado Estado {get; set;}
    public Prioridad Prioridad {get; set;}

    [Range(1, int.MaxValue, ErrorMessage = "La categoria no es válida")]
    public int CategoriaId {get; set;}

}