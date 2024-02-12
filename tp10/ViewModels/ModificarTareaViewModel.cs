using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using tp10.Models;
namespace tp10.ViewModels;

public class ModificarTareaViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Campo requerido")]
    public int IdTablero { get; set; }

    [Required(ErrorMessage = "El nombre de la tarea es obligatorio.")]
    [Display(Name = "Nombre de la Tarea")]
    public string? Nombre { get; set; }

    [Display(Name = "Descripción de la Tarea")]
    public string? Descripcion { get; set; }

    [Display(Name = "Estado de la Tarea")]
    public EstadoTarea Estado { get; set; }
    
    [Display(Name = "Color de la Tarea")]
    public string? Color { get; set; }


    public ModificarTareaViewModel()
    {
    }

    public ModificarTareaViewModel(Tarea tarea)
    {
        Id = tarea.Id;
        Nombre = tarea.Nombre;
        Estado = tarea.Estado;
        Descripcion = tarea.Descripcion;
        Color = tarea.Color;
    }

}