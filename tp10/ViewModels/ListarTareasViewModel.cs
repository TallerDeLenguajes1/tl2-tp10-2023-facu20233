using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using tp10.Models;
namespace tp10.ViewModels;

public class ListarTareasViewModel
{
    public List<Tarea> ListaTareas { get; set; }
    public List<Tablero> ListaTableros { get; set; }
    public List<Usuario> ListaUsuarios { get; set; }

    public ListarTareasViewModel(List<Tarea> listaTareas, List<Tablero> listaTableros, List<Usuario> listaUsuarios)
    {
        ListaTareas = listaTareas;
        ListaTableros = listaTableros;
        ListaUsuarios = listaUsuarios;
    }
}