// UsuariosController.cs

using Microsoft.AspNetCore.Mvc;
using tp10.Repositorios; // Asegúrate de que tengas la referencia correcta al espacio de nombres del repositorio
using tp10.Models;
using tp10.ViewModels;
namespace tp10.Controllers;
public class UsuarioController : Controller
{
    private readonly ILogger<UsuarioController> _logger;
    private IUsuarioRepository _usuarioRepository;

    public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _usuarioRepository = usuarioRepository;
    }

    // Acción para listar usuarios

    public IActionResult Index() //index nombre archivo
    {
        try
        {
            if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });

            var user = HttpContext.Session.GetString("Usuario");

            if (esAdmin())
            {
                var usuarios = _usuarioRepository.GetAll();
                return View(new ListarUsuariosViewModel(usuarios));
            }
            else
            {
                var usuarios = _usuarioRepository.GetAll();
                return View(new ListarUsuariosViewModel(usuarios));
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }


    [HttpGet]
    public IActionResult UpdateUsuario(int id)
    {
        try
        {
            if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" }); //43
            return View(new ModificarUsuarioViewModel(_usuarioRepository.Get(id)));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }

    }


    [HttpPost]
    public IActionResult UpdateUsuario(ModificarUsuarioViewModel usuario)
    {
        try
        {
            if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
            if (!ModelState.IsValid) return RedirectToAction("Index");

            _usuarioRepository.Update(usuario.Id, new Usuario(usuario));
            return RedirectToAction("Index");

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }


    // --------- Controles -----------

    public IActionResult Error()
    {
        return View(new ErrorViewModel());
    }

    private bool logueado()
    {
        return HttpContext.Session.Keys.Any();
    }

    private bool esAdmin()
    {

        bool sesionIniciada = HttpContext.Session.Keys.Any();
        string nivelAcceso = HttpContext.Session.GetString("Rol");
        bool esAdmin = nivelAcceso == Rol.Administrador.ToString();
        return sesionIniciada && esAdmin;

        // return HttpContext.Session.Keys.Any() && HttpContext.Session.GetString("NivelAcceso") == Rol.Administrador.ToString();

    }

}


// // Acción para mostrar la página de creación de usuarios, 
// [HttpGet]
// public IActionResult Crear() // metodo, view
// {
//     if (!manejoController.IsLogged(HttpContext)) return RedirectToAction("Index");
//     if (!manejoController.IsAdmin(HttpContext)) return RedirectToAction("Index");
//     return View(new CrearUsuarioViewModel());
// }

// // Acción para procesar la creación de usuarios
// [HttpPost]
// public IActionResult Crear(CrearUsuarioViewModel usuario)
// {
//     if (!manejoController.IsLogged(HttpContext)) return RedirectToAction("Index");
//     if (!ModelState.IsValid) return RedirectToAction("CreateUser");

//     usuarioRepository.Create(new Usuario(usuario));

//     // Redirigir a la acción Index después de crear el usuario
//     return RedirectToAction("Index");

// }

// // Acción para mostrar la página de modificación de usuarios
// [HttpGet]
// public IActionResult Modificar(int idUsuario)
// {
//     if (!manejoController.IsLogged(HttpContext)) return RedirectToAction("Index");
//     if (!manejoController.IsAdmin(HttpContext)) return RedirectToAction("Index");

//     var usuario = usuarioRepository.Get(idUsuario);

//     var usuarioVM = new ModificarUsuarioViewModel(usuario.Id);

//     return View(usuarioVM);
// }

// // Acción para procesar la modificación de usuarios
// [HttpPost]
// public IActionResult Modificar(ModificarUsuarioViewModel usuarioVM)
// {
//     if (!manejoController.IsLogged(HttpContext)) return RedirectToAction("Index");
//     if (!ModelState.IsValid) return RedirectToAction("index");

//     usuarioRepository.Update(usuarioVM.Id, new Usuario(usuarioVM));

//     return RedirectToAction("Index");
// }

// // Acción para eliminar usuarios
// public IActionResult Eliminar(int id)
// {
//     if (!manejoController.IsLogged(HttpContext)) return RedirectToAction("Index");
//     if (!manejoController.IsAdmin(HttpContext)) return RedirectToAction("Index");
//     usuarioRepository.Remove(id);
//     return RedirectToAction("Index");
// }