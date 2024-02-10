using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using tp10.Models;
namespace tp10.ViewModels;

public class CrearTareaViewModel
{
    public string Id { get; set; }

    public string IdUsuarioAsignado { get; set; }

    [Required(ErrorMessage = "El nombre de la tarea es obligatorio.")]
    [Display(Name = "Nombre de la Tarea")]
    public string Nombre { get; set; }

    [Display(Name = "Descripción de la Tarea")]
    public string Descripcion { get; set; }

    [Display(Name = "Color de la Tarea")]
    public string Color { get; set; }

    [Display(Name = "Estado de la Tarea")]
    public EstadoTarea Estado { get; set; }
    public List<Tablero> Tableros { get; internal set; } //*
    public int IdTablero { get; internal set; }
    public Tarea Tarea { get; internal set; }



    

    // Otros campos que puedan ser necesarios para la creación de tareas
}