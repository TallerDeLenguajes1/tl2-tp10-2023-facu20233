using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using tp10.Models;
namespace tp10.ViewModels;

public class CrearTableroViewModel
{
    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "usuario propietario")]
    public int IdUsuarioPropietario { get; set; }

    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Nombre del Tablero")]
    public string? Nombre { get; set; }

    [Display(Name = "Descripción del Tablero")]
    public string? Descripcion { get; set; }

    // 

    public List<Usuario>? ListaUsuarios { get; set; }
    // public List<SelectListItem>? Tableros { get; set; }

    public CrearTableroViewModel()
    {
        // 
    }

    public CrearTableroViewModel(Tablero tablero, List<Usuario> listaUsuarios)
    {
        this.IdUsuarioPropietario = tablero.IdUsuarioPropietario;
        this.Nombre = tablero.Nombre;
        this.Descripcion = tablero.Descripcion;
        ListaUsuarios = listaUsuarios;
    }


}