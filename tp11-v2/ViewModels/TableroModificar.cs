using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using tp10.Models;
namespace tp10.ViewModels;

public class ModificarTableroViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre del tablero es obligatorio.")]
    [Display(Name = "Nombre del Tablero")]
    public string Nombre { get; set; }

    [Display(Name = "Descripción del Tablero")]
    public string Descripcion { get; set; }

    // Otros campos que puedan ser necesarios para la modificación de tableros
}