namespace tp10.Models;
using tp10.ViewModels;
public enum Rol
{
    Operador,
    Administrador
}

public class Usuario{
    private int id;
    private string nombreDeUsuario;
    private string contrasenia;
    private Rol rol;

    public int Id { get => id; set => id = value; }
    public string NombreDeUsuario { get => nombreDeUsuario; set => nombreDeUsuario = value; }
    public string Contrasenia { get => contrasenia; set => contrasenia = value; }
    public Rol Rol { get => rol; set => rol = value; }

    public Usuario()
    {
    }

    // Constructor adicional para crear un Usuario desde un ViewModel de inicio de sesión
    public Usuario(LoginViewModel loginViewModel)
    {
        NombreDeUsuario = loginViewModel.nombreDeUsuario;
        Contrasenia = loginViewModel.contrasenia;
    }
}