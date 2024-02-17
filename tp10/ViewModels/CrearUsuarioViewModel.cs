using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using tp10.Models;
namespace tp10.ViewModels;

public class CrearUsuarioViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Campo obligatorio.")]
    [StringLength(30)]
    [Display(Name = "Nombre de Usuario")]
    public string? NombreDeUsuario { get; set; }

    [Required(ErrorMessage = "Campo obligatorio.")]
    [Display(Name = "Contraseña")]
    [DataType(DataType.Password)]
    public string? Contrasenia { get; set; }

    [Required(ErrorMessage = "Campo obligatorio.")]
    [Display(Name = "Rol")]
    public Rol Rol { get; set; }
    public bool EsAdmin { get; set; }
    public bool Logueado { get; set; }

}

