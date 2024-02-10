using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace tp10.ViewModels
{
    public class LoginViewModel
    {
        public string? MensajeDeError;

        // revisar
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Nombre de Usuario")] 
        public string? nombreDeUsuario {get;set;}        
        
        [Required(ErrorMessage = "Este campo es requerido.")]
        [PasswordPropertyText]
        [Display(Name = "Contraseña")]
        public string? contrasenia {get;set;}
    }
}