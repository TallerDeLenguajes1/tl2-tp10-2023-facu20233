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
            // if (!esAdmin()) return RedirectToRoute("Index");

            var userId = HttpContext.Session.GetInt32("Id") ?? 0;
            var user = _usuarioRepository.Get(userId);

            if (esAdmin())
            {
                var usuarios = _usuarioRepository.GetAll();
                return View(new ListarUsuariosViewModel(usuarios)
                {
                    EsAdmin = esAdmin(),
                    Logueado = logueado()
                });
            }
            else
            {
                ViewBag.EsAdmin = false;
                var usuarios = new List<Usuario>();
                usuarios.Add(user);
                return View(new ListarUsuariosViewModel(usuarios)
                {
                    EsAdmin = esAdmin()
                });
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
            var user = _usuarioRepository.Get(id);

            var viewModel = new ModificarUsuarioViewModel(user)
            {
                Logueado = true,
                EsAdmin = esAdmin()
            };

            return View(viewModel);
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

    public IActionResult DeleteUsuario(int id)
    {
        try
        {
            if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
            if (!ModelState.IsValid) return RedirectToAction("Index", id);

            if (esAdmin())
            {
                var idUsuario = _usuarioRepository.Get(id).Id;
                _usuarioRepository.Delete(id);
                return RedirectToAction("Index", new { id = idUsuario });
            }
            else
            {
                var idUsuario = _usuarioRepository.Get(id).Id;
                _usuarioRepository.Delete(id);
                DesloguearUsuario();
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpGet]
    public IActionResult CrearUsuario()
    {
        try
        {
            // if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
            var viewModel = new CrearUsuarioViewModel()
            {
                EsAdmin = esAdmin()
            };
            return View(viewModel);
        }

        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpPost]
    public IActionResult CrearUsuario(CrearUsuarioViewModel usuario)
    {
        try
        {
            if (!ModelState.IsValid) return RedirectToAction("Index");

            _usuarioRepository.Create(new Usuario(usuario));
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

    private void DesloguearUsuario()
    {
        HttpContext.Session.Clear();
    }

}


