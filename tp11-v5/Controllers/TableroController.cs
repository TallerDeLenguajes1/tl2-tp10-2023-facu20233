// TableroController.cs
using Microsoft.AspNetCore.Mvc;
using tp10.Repositorios;
using tp10.Models;
using tp10.ViewModels;
namespace tp10.Controllers;


public class TableroController : Controller
{
    private TableroRepository tableroRepository;
    private ManejoController manejoController;
    private UsuarioRepository usuarioRepository;


    // Constructor para inicializar el repositorio
    public TableroController()
    {
        tableroRepository = new TableroRepository();
        manejoController = new ManejoController();
        usuarioRepository = new UsuarioRepository();
    }

    // private bool isLogged()
    // {
    //     if (HttpContext.Session != null && (HttpContext.Session.GetString("NivelDeAcceso") == "admin" || HttpContext.Session.GetString("NivelDeAcceso") == "operador"))
    //         return true;

    //     return false;
    // }


    // Acción para listar tableros
    public IActionResult Index()
    {
        if (!manejoController.IsLogged(HttpContext)) return RedirectToAction("Index");
        

        if (manejoController.IsAdmin(HttpContext))
        {
            var tableros = tableroRepository.GetAll();
            return View(tableros);
        }
        else
        {
            var usuario = usuarioRepository.GetAll().FirstOrDefault(u => u.NombreDeUsuario == HttpContext.Session.GetString("Usuario") && u.Contrasenia == HttpContext.Session.GetString("Password"));
            var tablero = tableroRepository.GetByUser(usuario.Id);
            return View(tablero);
        }

    }


    // private bool isAdmin()
    // {
    //     if (HttpContext.Session != null && HttpContext.Session.GetString("NivelDeAcceso") == "admin")
    //         return true;

    //     return false;
    // }

    // [HttpGet]
    // // Acción para mostrar la página de creación de tableros
    // public IActionResult Crear()
    // {
    //     return View();
    // }

    // public IActionResult Tareas(int id)
    // {
    //     if (!manejoController.IsLogged(HttpContext)) return RedirectToAction("Index");
    //     var tareas = tableroRepository.GetTareasPorTablero(id);
    //     return View("TareasAsociadas", tareas);
    // }

    // public IActionResult TareasAsociadas(int id)
    // {
    //     if (!manejoController.IsLogged(HttpContext)) return RedirectToAction("Index");

    //     var tablero = tableroRepository.Get(id);

    //     if (tablero == null)
    //     {
    //         return NotFound(); // Devolver un error 404 si el tablero no se encuentra
    //     }

    //     var tareasAsociadas = tableroRepository.ObtenerTareasAsociadasAlTablero(id); // 
    //     return View(tareasAsociadas);
    // }

    [HttpGet]
    // Acción para mostrar la página de creación de tableros
    public IActionResult Crear()
    {
        if (!manejoController.IsLogged(HttpContext)) return RedirectToAction("Index");
        if (!manejoController.IsAdmin(HttpContext)) return RedirectToAction("Index");
        
        return View(new CrearTableroViewModel{IdUsuarioPropietario = (int)HttpContext.Session.GetInt32("id")});

    }


    // Acción para procesar la creación de tableros
    [HttpPost]
    public IActionResult Crear(CrearTableroViewModel tableroVM)
    {
        if (!manejoController.IsLogged(HttpContext)) return RedirectToAction("Index");
        if (!manejoController.IsAdmin(HttpContext)) return RedirectToAction("Index");
        if (!ModelState.IsValid) return RedirectToAction("Index");

        // Validar el modelo antes de intentar guardarlo
        tableroRepository.Create(new Tablero(tableroVM));

        // Redirigir a la acción Index después de crear el tablero

        // Si el modelo no es válido, vuelve a mostrar la vista de creación con errores
        return RedirectToAction("Index"); 
    }

    // Acción para mostrar la página de modificación de tableros
    [HttpGet]
    public IActionResult Modificar(int IdTablero)
    {
        if (!manejoController.IsLogged(HttpContext)) return RedirectToAction("Index");
        if (!manejoController.IsAdmin(HttpContext)) return RedirectToAction("Index");

        return View(new ModificarTableroViewModel(tableroRepository.Get(IdTablero)));
    }

    // Acción para procesar la modificación de tableros
    [HttpPost]
    public IActionResult Modificar(ModificarTableroViewModel tableroVM)
    {
        if (!manejoController.IsLogged(HttpContext)) return RedirectToAction("Index");
        if (!manejoController.IsAdmin(HttpContext)) return RedirectToAction("Index");
        if (!ModelState.IsValid) return RedirectToAction("Index");

        // var tablero = new Tablero(tableroVM);

        tableroRepository.Update(tableroVM.Id, new Tablero(tableroVM));
        return RedirectToAction("Index");
    }

    // Acción para eliminar tableros
    public IActionResult Eliminar(int id)
    {
        if (!manejoController.IsLogged(HttpContext)) return RedirectToAction("Index");
        if (!manejoController.IsAdmin(HttpContext)) return RedirectToAction("Index");
        tableroRepository.Remove(id);
        return RedirectToAction("Index");
    }
}
