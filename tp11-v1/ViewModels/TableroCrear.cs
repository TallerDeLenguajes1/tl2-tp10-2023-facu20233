using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using tp10.Models;
namespace tp10.ViewModels;

public class CrearTableroViewModel
{
    [Required(ErrorMessage = "El nombre del tablero es obligatorio.")]
    [Display(Name = "Nombre del Tablero")]
    public string Nombre { get; set; }

    [Display(Name = "Descripción del Tablero")]
    public string Descripcion { get; set; }

    [Required(ErrorMessage = "Debe seleccionar un tablero.")]
    [Display(Name = "Tablero")]
    public int IdTablero { get; set; }

    // Propiedad adicional para almacenar la lista de tableros
    public List<SelectListItem> Tableros { get; set; }

    // Otros campos que puedan ser necesarios para la creación de tableros
}