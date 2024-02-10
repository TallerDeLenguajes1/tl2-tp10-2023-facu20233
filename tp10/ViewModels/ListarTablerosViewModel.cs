using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using tp10.Models;
namespace tp10.ViewModels;

public class ListarTablerosViewModel
{
    public List<Tablero> Listatableros { get; set; }

    public ListarTablerosViewModel(List<Tablero> listatableros)
    {
        Listatableros = listatableros;
    }

}

