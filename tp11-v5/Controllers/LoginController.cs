using Microsoft.AspNetCore.Mvc;
using tp10.Models;
using tp10.Repositorios;
using tp10.ViewModels;

namespace tp10.Controllers;

public class LoginController : Controller
{
    // List<Usuario> Usuarios = new List<Usuario>();
    private readonly UsuarioRepository usuarioRepository;
    private readonly TableroRepository tableroRepository;


    private readonly ILogger<LoginController> _logger;
    public LoginController(ILogger<LoginController> logger)
    {
        _logger = logger;
        usuarioRepository = new UsuarioRepository();
        tableroRepository = new TableroRepository();

        // var usuarioAdmin = new Usuario()
        // {
        //     NombreDeUsuario = "admin",
        //     Contrasenia = "admin",
        //     Rol = Rol.Administrador
        // };

        // var usuarioSimple = new Usuario()
        // {
        //     NombreDeUsuario = "simple",
        //     Contrasenia = "simple",
        //     Rol = Rol.Operador
        // };
        // Usuarios.Add(usuarioAdmin);
        // Usuarios.Add(usuarioSimple);

    }

    public IActionResult Index()
    {
        return View(new LoginViewModel());
    }


    public IActionResult Login(Usuario usuario)
    {
        // Existe el usuario?
        var usuarioLogeado = usuarioRepository.GetUsuario(usuario.NombreDeUsuario, usuario.Contrasenia);

        // Si el usuario no existe, devuelve al index
        if (usuarioLogeado == null) return RedirectToAction("Index");

        // Registro el usuario
        logearUsuario(usuarioLogeado);

        if (usuarioLogeado.Rol == Rol.Administrador)
        {
            // Si el rol es administrador, puede ver todos los tableros
            return RedirectToAction("Index", "Tablero");
        }
        else if (usuarioLogeado.Rol == Rol.Operador)
        {
            // Obtener los tableros del usuario
            var tablerosDelUsuario = tableroRepository.GetByUser(usuarioLogeado.Id);

            // Verificar si el operador intenta ver tareas de otro usuario
            if (tablerosDelUsuario.Any(t => t.Id == usuarioLogeado.Id))
            {
                // El operador está intentando ver tareas de otro usuario, devuelve un error
                return NotFound(); // O puedes devolver Forbid() para devolver un 403 Forbidden
            }

            // Si el rol es operador, solo puede ver sus tableros
            return RedirectToAction("Index", "Tablero", new { idUsuario = usuarioLogeado.Id });
        }

        // Devuelvo el usuario a la página de Usuarios
        return RedirectToAction("Home");
    }

    private void logearUsuario(Usuario user)
    {
        HttpContext.Session.SetString("Usuario", user.NombreDeUsuario);
        HttpContext.Session.SetString("NivelDeAcceso", user.Contrasenia);
        HttpContext.Session.SetString("NivelAcceso", user.Rol.ToString());
    }
}