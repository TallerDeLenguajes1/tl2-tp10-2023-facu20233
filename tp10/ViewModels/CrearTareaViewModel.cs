using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using tp10.Models;
namespace tp10.ViewModels;

public class CrearTareaViewModel
{
    public int Id { get; set; }

    [Display(Name = "Id del Tablero")]
    public int IdTablero { get; set; }
    

    [Required(ErrorMessage = "El nombre de la tarea es obligatorio.")]
    [Display(Name = "Nombre de la Tarea")]
    public string? Nombre { get; set; }

    [Display(Name = "Estado de la Tarea")]
    public EstadoTarea Estado { get; set; }

    [Display(Name = "Descripción de la Tarea")]
    public string? Descripcion { get; set; }

    [Display(Name = "Color de la Tarea")]
    public string? Color { get; set; }
    public int IdUsuarioAsignado { get; set; }

    // ---
    public List<Tablero>? Tableros { get; set; } //*
    public Tarea? Tarea { get; set; }

    
}