using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using tp10.Models;

namespace tp10.ViewModels
{
    public class ModificarTableroViewModel
    {
        public int Id { get; set; }

        // public List<Usuario> Usuarios { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        public int IdUsuarioPropietario { get; set; }

        [Required(ErrorMessage = "El nombre del tablero es obligatorio.")]
        [Display(Name = "Nombre del Tablero")]
        public string Nombre { get; set; }

        [Display(Name = "Descripción del Tablero")]
        public string Descripcion { get; set; }

        public ModificarTableroViewModel()
        {
            // Puedes asignar valores predeterminados aquí si es necesario
        }

        // public ModificarTableroViewModel(int IdTablero, List<Usuario> users)
        // {
        //     Id = IdTablero;
        //     Usuarios = users;
        // }

        // public ModificarTableroViewModel(Tablero tablero)
        // {
        //     Id = tablero.Id;
        //     Nombre = tablero.Nombre;
        //     Descripcion = tablero.Descripcion;

        //     // Inicializa otras propiedades según sea necesario
        // }

        public ModificarTableroViewModel(Tablero tablero){
            this.Id = tablero.Id;
            this.IdUsuarioPropietario = tablero.IdUsuarioPropietario;
            this.Nombre = tablero.Nombre;
            this.Descripcion = tablero.Descripcion;
        }
    }
}
