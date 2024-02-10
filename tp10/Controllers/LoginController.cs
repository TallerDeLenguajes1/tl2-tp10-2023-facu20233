using Microsoft.AspNetCore.Mvc;
using tp10.Models;
using tp10.Repositorios;
using tp10.ViewModels;

namespace tp10.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;
    private readonly IUsuarioRepository _usuarioRepository;

    public LoginController(ILogger<LoginController> logger, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _usuarioRepository = usuarioRepository;

    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(new LoginViewModel());
    }


    public IActionResult Login(UsuarioViewModel usuario)
    {
        try
        {
            //existe el usuario?
            var usuarioLogeado = _usuarioRepository.AutenticarUsuario(usuario.NombreDeUsuario, usuario.Contrasenia);

            // si el usuario no existe devuelvo al index
            if (usuarioLogeado == null)
            {
                var loginVM = new LoginViewModel()
                {
                    MensajeDeError = "Usuario no existente"
                };
                return View("Index", loginVM);
            }

            logearUsuario(usuario);

        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al intentar logear un usuario {ex.ToString()}");

        }

        return RedirectToRoute(new { controller = "Home", action = "Index" });
    }

    private void logearUsuario(UsuarioViewModel user)
    {
        HttpContext.Session.SetString("Usuario", user.NombreDeUsuario);
        HttpContext.Session.SetString("Contrasenia", user.Contrasenia);
        HttpContext.Session.SetString("NivelAcceso", user.Rol.ToString());
    }
}