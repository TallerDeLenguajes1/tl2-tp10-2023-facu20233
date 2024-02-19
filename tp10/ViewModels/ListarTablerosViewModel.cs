using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using tp10.Models;
namespace tp10.ViewModels;

public class ListarTablerosViewModel
{
    public List<Tablero> Listatableros { get; set; }
    public List<Tablero>? ListatablerosOtros { get; set; }
    public List<Usuario> ListaUsuarios { get; set; }

    public ListarTablerosViewModel(List<Tablero> listatableros, List<Usuario> listaUsuarios)
    {
        Listatableros = listatableros;
        ListaUsuarios = listaUsuarios;
        // ListatablerosOtros = listatablerosOtros;
    }
    public ListarTablerosViewModel(List<Tablero> listatableros, List<Tablero> listatablerosOtros, List<Usuario> listaUsuarios)
    {
        Listatableros = listatableros;
        ListaUsuarios = listaUsuarios;
        ListatablerosOtros = listatablerosOtros;
    }

}

