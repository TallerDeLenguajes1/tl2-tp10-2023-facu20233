// UsuarioListar.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using tp10.Models;
namespace tp10.ViewModels;

public class ListarUsuariosViewModel
{
    public List<Usuario> ListaUsuarios { get; set; }

    public bool EsAdmin { get; set; }

    public ListarUsuariosViewModel(List<Usuario> listaUsuarios)
    {
        ListaUsuarios = listaUsuarios;
    }
}