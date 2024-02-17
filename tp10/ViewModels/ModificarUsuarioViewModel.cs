using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using tp10.Models;
namespace tp10.ViewModels;

public class ModificarUsuarioViewModel
{
    public int Id { get; set; }

    [Display(Name = "Nombre de Usuario")]
    [StringLength(30)]
    [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
    public string? NombreDeUsuario { get; set; }

    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [DataType(DataType.Password)]
    [StringLength(30)]
    [Display(Name = "Contraseña")]
    public string? Contrasenia { get; set; }

    [Display(Name = "Rol")]
    [Required(ErrorMessage = "El rol es obligatorio.")]
    public Rol Rol { get; set; }
    public bool EsAdmin { get; set; }
    public bool Logueado { get; set; }

    public ModificarUsuarioViewModel(Usuario usuario)
        {
            NombreDeUsuario = usuario.NombreDeUsuario;
            Contrasenia = usuario.Contrasenia;
            Rol = usuario.Rol;
        }

        public ModificarUsuarioViewModel()
        {
        }
    
}