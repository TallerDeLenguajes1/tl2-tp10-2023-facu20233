// TableroController.cs
using Microsoft.AspNetCore.Mvc;
using tp10.Repositorios;
using tp10.Models;
using tp10.ViewModels;

namespace tp10.Controllers;

public class TableroController : Controller
{

    private readonly ILogger<TableroController> _logger;

    private readonly ITableroRepository _tableroRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    // private readonly ITareaRepository _tareaRepository;


    public TableroController(ILogger<TableroController> logger, ITableroRepository tableroRepository, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _tableroRepository = tableroRepository;
        _usuarioRepository = usuarioRepository;
        // _tareaRepository = tareaRepository;
    }


    // Acción para listar tableros
    public IActionResult Index()
    {
        try
        {
            if (!logueado()) return RedirectToAction("Index");
            var user = HttpContext.Session.GetString("Usuario");

            if (esAdmin())
            {
                var tableros = _tableroRepository.GetAll();
                return View(new ListarTablerosViewModel(tableros));
            }
            else
            {
                var tableros = _tableroRepository.GetAll();
                return View(new ListarTablerosViewModel(tableros));
                // var usuario = _usuarioRepository.GetAll().FirstOrDefault(u => u.NombreDeUsuario == HttpContext.Session.GetString("Usuario") && u.Contrasenia == HttpContext.Session.GetString("Contrasenia"));
                // var tablero = _tableroRepository.GetByUser(usuario.Id);
                // return View(tablero);
            }

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }

    }


    // [HttpGet]
    // // Acción para mostrar la página de creación de tableros
    // public IActionResult Crear()
    // {
    //     if (!manejoController.IsLogged(HttpContext)) return RedirectToAction("Index");
    //     if (!manejoController.IsAdmin(HttpContext)) return RedirectToAction("Index");

    //     return View(new CrearTableroViewModel{IdUsuarioPropietario = (int)HttpContext.Session.GetInt32("id")});

    // }


    // // Acción para procesar la creación de tableros
    // [HttpPost]
    // public IActionResult Crear(CrearTableroViewModel tableroVM)
    // {
    //     if (!manejoController.IsLogged(HttpContext)) return RedirectToAction("Index");
    //     if (!manejoController.IsAdmin(HttpContext)) return RedirectToAction("Index");
    //     if (!ModelState.IsValid) return RedirectToAction("Index");

    //     // Validar el modelo antes de intentar guardarlo
    //     _tableroRepository.Create(new Tablero(tableroVM));

    //     // Redirigir a la acción Index después de crear el tablero

    //     // Si el modelo no es válido, vuelve a mostrar la vista de creación con errores
    //     return RedirectToAction("Index"); 
    // }

    // // Acción para mostrar la página de modificación de tableros
    // [HttpGet]
    // public IActionResult Modificar(int IdTablero)
    // {
    //     if (!manejoController.IsLogged(HttpContext)) return RedirectToAction("Index");
    //     if (!manejoController.IsAdmin(HttpContext)) return RedirectToAction("Index");

    //     return View(new ModificarTableroViewModel(_tableroRepository.Get(IdTablero)));
    // }

    // // Acción para procesar la modificación de tableros
    // [HttpPost]
    // public IActionResult Modificar(ModificarTableroViewModel tableroVM)
    // {
    //     if (!manejoController.IsLogged(HttpContext)) return RedirectToAction("Index");
    //     if (!manejoController.IsAdmin(HttpContext)) return RedirectToAction("Index");
    //     if (!ModelState.IsValid) return RedirectToAction("Index");

    //     // var tablero = new Tablero(tableroVM);

    //     _tableroRepository.Update(tableroVM.Id, new Tablero(tableroVM));
    //     return RedirectToAction("Index");
    // }

    // // Acción para eliminar tableros
    // public IActionResult Eliminar(int id)
    // {
    //     if (!manejoController.IsLogged(HttpContext)) return RedirectToAction("Index");
    //     if (!manejoController.IsAdmin(HttpContext)) return RedirectToAction("Index");
    //     _tableroRepository.Remove(id);
    //     return RedirectToAction("Index");
    // }

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
        return HttpContext.Session.Keys.Any() && HttpContext.Session.GetString("NivelAcceso") == Rol.Administrador.ToString();
    }

}
