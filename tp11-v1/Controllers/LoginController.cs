using Microsoft.AspNetCore.Mvc;
using tp10.Models;
using tp10.ViewModels;

// repositorio 2023
namespace tp10.Controllers;

public class LoginController : Controller
{
    List<Usuario> Usuarios = new List<Usuario>();

    private readonly ILogger<LoginController> _logger;
    public LoginController(ILogger<LoginController> logger)
    {
        _logger = logger;

          var usuarioAdmin = new Usuario()
        {
         NombreDeUsuario = "admin",
         Contrasenia1 = "admin",
         Rol = Rol.administrador
        };

        var usuarioSimple = new Usuario()
        {
         NombreDeUsuario = "simple",
         Contrasenia1 = "simple",
         Rol = Rol.operador
        };
        Usuarios.Add(usuarioAdmin);
        Usuarios.Add(usuarioSimple);
    }

    public IActionResult Index()
    {
        return View(new LoginViewModel());
    }


    public IActionResult Login(Usuario usuario)
    {
        //existe el usuario?
        var usuarioLogeado = Usuarios.FirstOrDefault(u => u.NombreDeUsuario == usuario.NombreDeUsuario && u.Contrasenia1 == usuario.Contrasenia1);

        // si el usuario no existe devuelvo al index
        if (usuarioLogeado == null) return RedirectToAction("Index");
        
        //Registro el usuario
        logearUsuario(usuarioLogeado);
        
        //Devuelvo el usuario al Home
        return RedirectToRoute(new { controller = "Home", action = "Index" });
    }

    private void logearUsuario(Usuario user)
    {
        HttpContext.Session.SetString("Usuario", user.NombreDeUsuario);
        HttpContext.Session.SetString("NivelDeAcceso", user.Contrasenia1);
        HttpContext.Session.SetString("NivelAcceso", user.Rol.ToString());
    }
}