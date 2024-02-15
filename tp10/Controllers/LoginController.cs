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


    public IActionResult Login(LoginViewModel usuario)
    {
        try
        {
            if (!ModelState.IsValid) return RedirectToAction("Index"); //*

            //existe el usuario?
            var usuarioLogeado = _usuarioRepository.AutenticarUsuario(usuario.nombreDeUsuario, usuario.contrasenia);

            // si el usuario no existe devuelvo al index
            if (usuarioLogeado == null)
            {
                var loginVM = new LoginViewModel()
                {
                    MensajeDeError = "Usuario no existente"
                };
                return View("Index", loginVM);
            }

            logearUsuario(usuarioLogeado);
            _logger.LogInformation("El usuario {0} ingreso correctamente", usuario.nombreDeUsuario); //*

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            _logger.LogWarning("Intento de acceso invalido - Usuario: {0} Clave ingresada: {1}", usuario.nombreDeUsuario, usuario.contrasenia);
            return RedirectToAction("Index");
        }

    }

    private void logearUsuario(Usuario user)
    {
        HttpContext.Session.SetString("Usuario", user.NombreDeUsuario);
        HttpContext.Session.SetString("Contrasenia", user.Contrasenia);
        HttpContext.Session.SetString("Rol", user.Rol.ToString());
    }
}