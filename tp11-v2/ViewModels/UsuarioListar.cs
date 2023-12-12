using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using tp10.Models;
namespace tp10.ViewModels;

public class ListarUsuariosViewModel
{
    public List<UsuarioViewModel> Usuarios { get; set; }
}