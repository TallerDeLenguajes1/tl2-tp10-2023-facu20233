// UsuarioListar.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using tp10.Models;
namespace tp10.ViewModels;

public class ListarUsuariosViewModel
{
    public List<Usuario>? ListaUsuarios { get; set; }
    public Usuario? Usuarios { get; set; }

    public bool EsAdmin { get; set; }
    public bool Logueado { get; set; }

    public ListarUsuariosViewModel(List<Usuario> listaUsuarios)
    {
        ListaUsuarios = listaUsuarios;
    }

    public ListarUsuariosViewModel(Usuario usuarios)
    {
        Usuarios = usuarios;
    }
}