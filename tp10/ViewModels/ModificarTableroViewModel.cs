using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using tp10.Models;
using Microsoft.AspNetCore.Mvc;

namespace tp10.ViewModels
{
    public class ModificarTableroViewModel
    {
        
        public int Id { get; set; }

        // public List<Usuario> Usuarios { get; set; }
        [HiddenInput(DisplayValue = false)]
        [Required(ErrorMessage = "Campo obligatorio")]
        [Display(Name = "Usuario Propietario")]
        public int IdUsuarioPropietario { get; set; }


        [Required(ErrorMessage = "Campo obligatorio")]
        [StringLength(30)]
        [Display(Name = "Nombre del Tablero")]
        public string? Nombre { get; set; }


        [Display(Name = "Descripción del Tablero")]
        [StringLength(50)]
        public string? Descripcion { get; set; }

        public List<Usuario>? ListaUsuarios { get; set; }

        public bool EsAdmin { get; set; }

        public ModificarTableroViewModel()
        {
        
        }

        public ModificarTableroViewModel(Tablero tablero){
            this.Id = tablero.Id;
            this.IdUsuarioPropietario = tablero.IdUsuarioPropietario;
            this.Nombre = tablero.Nombre;
            this.Descripcion = tablero.Descripcion;

        }

        public ModificarTableroViewModel(Tablero tablero, List<Usuario> listaUsuarios){
            this.Id = tablero.Id;
            this.IdUsuarioPropietario = tablero.IdUsuarioPropietario;
            this.Nombre = tablero.Nombre;
            this.Descripcion = tablero.Descripcion;

            ListaUsuarios = listaUsuarios;
        }
    }
}
