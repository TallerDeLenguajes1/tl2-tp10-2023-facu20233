using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using tp10.Models;
namespace tp10.ViewModels;

public class CrearTableroViewModel
{
    [Required(ErrorMessage = "Debe seleccionar un tablero.")]
    [Display(Name = "usuario propietario")]
    public int IdUsuarioPropietario { get; set; }

    [Required(ErrorMessage = "El nombre del tablero es obligatorio.")]
    [Display(Name = "Nombre del Tablero")]
    public string? Nombre { get; set; }

    [Display(Name = "Descripción del Tablero")]
    public string? Descripcion { get; set; }

    // 

    public List<Usuario>? Usuarios { get; set; }
    // public List<SelectListItem>? Tableros { get; set; }

    public CrearTableroViewModel()
    {
        // 
    }

    public CrearTableroViewModel(Tablero tablero)
    {
        this.IdUsuarioPropietario = tablero.IdUsuarioPropietario;
        this.Nombre = tablero.Nombre;
        this.Descripcion = tablero.Descripcion;
    }


}