// TareaController.cs

using Microsoft.AspNetCore.Mvc;
using tp10.Repositorios;
using tp10.Models;
namespace tp10.Controllers;

public class TareaController : Controller
{
    private TareaRepository tareaRepository;
    private ManejoController manejoController;
    private TableroRepository tableroRepository;

    // Constructor para inicializar el repositorio
    public TareaController()
    {
        tareaRepository = new TareaRepository();
        manejoController = new ManejoController();
        tableroRepository = new TableroRepository();
    }

    // Acción para listar tareas
    public IActionResult Index()
    {
        if (!manejoController.IsLogged(HttpContext)) return RedirectToAction("Index");
        var tareas = tareaRepository.GetAll();
        return View(tareas);
    }

    // public IActionResult Tareas(int id)
    // {
    //     if (!manejoController.IsLogged(HttpContext)) return RedirectToAction("Index");

    //     var tareas = tareaRepository.GetTareasPorTablero(id);

    //     return View(tareas);
    // }

    // Acción para mostrar la página de creación de tareas

    public IActionResult TareasAsociadas(int id)
    {
        if (!manejoController.IsLogged(HttpContext)) return RedirectToAction("Index");

        var tablero = tableroRepository.Get(id);

        if (tablero == null)
        {
            return NotFound(); // Devolver un error 404 si el tablero no se encuentra
        }
        
        var tareasAsociadas = tareaRepository.ObtenerTareasAsociadasAlTablero(id); // 
        return View(tareasAsociadas);
    }

    public IActionResult Crear()
    {
        return View();
    }

    // Acción para procesar la creación de tareas
    [HttpPost]
    public IActionResult Crear(int idTablero, Tarea tarea)
    {
        if (ModelState.IsValid)
        {
            // Validar el modelo antes de intentar guardarlo
            tareaRepository.Create(idTablero, tarea);

            // Redirigir a la acción Index después de crear la tarea
            return RedirectToAction("Index");
        }

        // Si el modelo no es válido, vuelve a mostrar la vista de creación con errores
        return View(tarea);
    }

    // Acción para mostrar la página de modificación de tareas
    public IActionResult Modificar(int id)
    {
        if (!manejoController.IsAdmin(HttpContext)) return RedirectToAction("Index");
        var tarea = tareaRepository.Get(id);
        return View(tarea);
    }

    // Acción para procesar la modificación de tareas
    [HttpPost]
    public IActionResult Modificar(Tarea tarea)
    {
        tareaRepository.Update(tarea.Id, tarea);
        return RedirectToAction("Index");
    }

    // Acción para eliminar tareas
    public IActionResult Eliminar(int id)
    {
        tareaRepository.Remove(id);
        return RedirectToAction("Index");
    }
}
