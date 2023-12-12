using tp10.ViewModels;
namespace tp10.Models;
public enum Rol
{
    operador = 1,
    administrador = 2
}

public class Usuario
{
    private int id;
    private string nombreDeUsuario;
    private string Contrasenia;
    private Rol rol;

    public int Id { get => id; set => id = value; }
    public string NombreDeUsuario { get => nombreDeUsuario; set => nombreDeUsuario = value; }
    public string Contrasenia1 { get => Contrasenia; set => Contrasenia = value; }
    public Rol Rol { get => rol; set => rol = value; }

    public Usuario(LoginViewModel loginViewModel)
    {
        nombreDeUsuario = loginViewModel.NombreDeUsuario;
        Contrasenia = loginViewModel.Contrasenia;
    }

    public Usuario()
    {
    }
}