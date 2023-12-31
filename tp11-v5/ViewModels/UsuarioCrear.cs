using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using tp10.Models;
namespace tp10.ViewModels;

public class CrearUsuarioViewModel
{
    public int Id { get; set; }

    [Display(Name = "Nombre de Usuario")]
    [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
    public string NombreDeUsuario { get; set; }

    [Display(Name = "Contraseña")]
    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [DataType(DataType.Password)]
    public string Contrasenia { get; set; }

    [Display(Name = "Rol")]
    [Required(ErrorMessage = "El rol es obligatorio.")]
    public Rol Rol { get; set; }

    // Otros campos que puedan ser necesarios para la creación de usuarios
}