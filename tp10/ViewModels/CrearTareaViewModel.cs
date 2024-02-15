using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using tp10.Models;
namespace tp10.ViewModels;

public class CrearTareaViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Campo obligatorio")]
    [Display(Name = "Nombre del Tablero")]
    public int IdTablero { get; set; }


    [Required(ErrorMessage = "Campo obligatorio")]
    [StringLength(30)]
    [Display(Name = "Nombre de la Tarea")]
    public string? Nombre { get; set; }

    [Required(ErrorMessage = "Campo obligatorio")]
    [Display(Name = "Estado de la Tarea")]
    public EstadoTarea Estado { get; set; }

    [StringLength(50)]
    [Display(Name = "Descripción de la Tarea")]
    public string? Descripcion { get; set; }

    [Display(Name = "Color de la Tarea")]
    public string? Color { get; set; }
    public int IdUsuarioAsignado { get; set; }

    // ---
    public List<Tablero>? ListaTableros { get; set; } //*
    public List<Usuario>? ListaUsuarios { get; set; } //*
    // public Tarea? Tarea { get; set; }

    public CrearTareaViewModel()
    {
        // 
    
    }
    public CrearTareaViewModel(List<Tablero> listaTableros, List<Usuario> listaUsuarios)
    {
        ListaTableros = listaTableros;
        ListaUsuarios = listaUsuarios;
    }

}