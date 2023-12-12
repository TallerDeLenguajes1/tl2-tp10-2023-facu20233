using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using tp10.Models;
namespace tp10.ViewModels;

public class UsuarioViewModel
{
    public int Id { get; set; }

    [Display(Name = "Nombre de Usuario")]
    public string NombreDeUsuario { get; set; }

    // Otros campos que puedan ser necesarios para mostrar información de usuario
}
