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

            if (usuarioLogeado == null)
            {
                var loginVM = new LoginViewModel()
                {
                    MensajeDeError = "¡Usuario no existe!"
                };
                return View("Index", loginVM);
            }

            logearUsuario(usuarioLogeado);
            _logger.LogInformation("El usuario {0} ingreso correctamente", usuario.nombreDeUsuario); //*

            return RedirectToRoute(new { controller = "Tablero", action = "Index" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            _logger.LogWarning("Intento de acceso invalido - Usuario: {0} Clave ingresada: {1}", usuario.nombreDeUsuario, usuario.contrasenia);

            var errorViewModel = new ErrorViewModel()
            {
                ErrorMessage = "¡Usuario no existe!"
            };
            return View("Error", errorViewModel);
        }
    }

    public IActionResult Logout()
    {
        try
        {
            DesloguearUsuario();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al intentar cerrar sesión del usuario {ex.ToString()}");
        }
        return RedirectToRoute(new { controller = "Login", action = "Index" });
    }

    private void logearUsuario(Usuario user)
    {
        HttpContext.Session.SetInt32("Id", user.Id);  //*
        HttpContext.Session.SetString("Usuario", user.NombreDeUsuario);
        HttpContext.Session.SetString("Contrasenia", user.Contrasenia);
        HttpContext.Session.SetString("Rol", user.Rol.ToString());
    }

    private void DesloguearUsuario()
    {
        HttpContext.Session.Clear();
    }
}