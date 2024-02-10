using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using tp10.Models;
namespace tp10.ViewModels;

public class TableroViewModel
{
    public int Id { get; set; }

    [Display(Name = "Nombre del Tablero")]
    public string Nombre { get; set; }

    [Display(Name = "Descripción del Tablero")]
    public string Descripcion { get; set; }

    public TableroViewModel(Tablero tablero)
        {  
            Id = tablero.Id;         
            Nombre = tablero.Nombre;
            Descripcion = tablero.Descripcion;        
        }

        public TableroViewModel()
        {
        }
}