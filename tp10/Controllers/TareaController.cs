// TableroController.cs
using Microsoft.AspNetCore.Mvc;
using tp10.Repositorios;
using tp10.Models;
using tp10.ViewModels;

namespace tp10.Controllers;

public class TareaController : Controller
{

    private readonly ILogger<TareaController> _logger;

    private readonly ITareaRepository _tareaRepository;
    private readonly ITableroRepository _tableroRepository;
    private readonly IUsuarioRepository _usuarioRepository;


    public TareaController(ILogger<TareaController> logger, ITareaRepository tareaRepository, ITableroRepository tableroRepository, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _tableroRepository = tableroRepository;
        _usuarioRepository = usuarioRepository;
        _tareaRepository = tareaRepository;
    }

    public IActionResult Index()
    {

        try
        {
            if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });

            var nombreUser = HttpContext.Session.GetString("Usuario");
            var user = _usuarioRepository.GetNombre(nombreUser);

            if (esAdmin())
            {
                ViewBag.AdminMessage = "¡Logueado como administrador!";
                var tareas = _tareaRepository.GetAll();
                return View(new ListarTareasViewModel(tareas));

            }
            else
            {
                ViewBag.AdminMessage = "¡Logueado como operador!";
                var tareas = _tareaRepository.GetByUser(user.Id);
                return View(new ListarTareasViewModel(tareas));
            }

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpGet]
    public IActionResult UpdateTarea(int id)
    {

        try
        {
            if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
            // if (!esAdmin()) return RedirectToRoute(new { controller = "Login", action = "Index" });

            var tareaVM = new ModificarTareaViewModel(_tareaRepository.Get(id));
            return View(tareaVM);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }


    }

    [HttpPost]
    public IActionResult UpdateTarea(ModificarTareaViewModel tarea)
    {
        try
        {
            if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
            if (!ModelState.IsValid) return RedirectToAction("Index");

            _tareaRepository.Update(tarea.Id, new Tarea(tarea));
            return RedirectToAction("Index");
            // return RedirectToAction("TareasAsociadas", new { id = tarea.IdTablero });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    public IActionResult DeleteTarea(int id)
    {

        try
        {
            if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
            if (!ModelState.IsValid) return RedirectToAction("Index", id);

            var idTablero = _tareaRepository.Get(id).IdTablero;
            _tareaRepository.Delete(id);
            return RedirectToAction("TareasAsociadas", new { id = idTablero });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }

    }

    [HttpGet]
    public IActionResult CrearTarea()
    {
        try
        {

            if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });

            var tableros = _tableroRepository.GetAll();
            var usuarios = _usuarioRepository.GetAll();

            var viewModel = new CrearTareaViewModel
            {
                ListaTableros = tableros,
                ListaUsuarios = usuarios
            };

            // var viewModel = new CrearTareaViewModel();
            return View(viewModel);
        }

        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }


    [HttpPost]
    public IActionResult CrearTarea(CrearTareaViewModel tarea)
    {
        try
        {
            if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
            if (!ModelState.IsValid) return RedirectToAction("Index");

            _tareaRepository.Create(new Tarea(tarea));
            return RedirectToAction("Index");


        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpGet]
    public IActionResult AgregarTarea(int id)
    {
        try
        {

            if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });

            var usuarios = _usuarioRepository.GetAll();
            var id_Tablero = _tableroRepository.Get(id).Id;

            var viewModel = new CrearTareaViewModel
            {
                IdTablero = id_Tablero,
                ListaUsuarios = usuarios
            };

            // var viewModel = new CrearTareaViewModel();
            return View(viewModel);
        }

        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }


    [HttpPost]
    public IActionResult AgregarTarea(CrearTareaViewModel tarea)
    {
        try
        {
            if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
            if (!ModelState.IsValid) return RedirectToAction("Index");

            _tareaRepository.CreateEnTablero(tarea.IdTablero, new Tarea(tarea));

            // return RedirectToAction("TareasAsociadas", new{id = tarea.IdTablero}); //
            return RedirectToRoute(new { controller = "Tablero", action = "Index" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    // 
    // 

    [HttpGet]
    public IActionResult TareasAsociadas(int id)
    {
        try
        {
            if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });

            var tablero = _tableroRepository.Get(id).Id;
            var tareas = _tableroRepository.ObtenerTareasAsociadasAlTablero(tablero);

            
            return View(new ListarTareasViewModel(tareas));
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


// public IActionResult Tareas(int id)
// {
//     if (!manejoController.IsLogged(HttpContext)) return RedirectToAction("Index");

//     var tareas = tareaRepository.GetTareasPorTablero(id);

//     return View(tareas);
// }

// Acción para mostrar la página de creación de tareas

// public IActionResult TareasAsociadas(int id)
// {
//     if (!manejoController.IsLogged(HttpContext)) return RedirectToAction("Index");

//     var tablero = tableroRepository.Get(id);

//     if (tablero == null)
//     {
//         return NotFound(); // Devolver un error 404 si el tablero no se encuentra
//     }

//     var tareasAsociadas = tareaRepository.ObtenerTareasAsociadasAlTablero(id); // 
//     return View(tareasAsociadas);
// }

// public IActionResult Crear()
// {
//     if (!manejoController.IsLogged(HttpContext)) return RedirectToAction("Index");
//     var TareaVM = new CrearTareaViewModel();
//     return View(TareaVM);
// }

// // Acción para procesar la creación de tareas
// [HttpPost]
// public IActionResult Crear(CrearTareaViewModel TareaVM)
// {
//     if (!manejoController.IsLogged(HttpContext)) return RedirectToAction("Index");
//     if (!ModelState.IsValid) return RedirectToAction("Index");

//     tareaRepository.Create(TareaVM.IdTablero, new Tarea(TareaVM));

//     return RedirectToAction("TareasAsociadas", new { idTablero = TareaVM.IdTablero });
// }



