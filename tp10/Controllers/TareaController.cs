// TareaController.cs

using Microsoft.AspNetCore.Mvc;
using tp10.Repositorios;
using tp10;

public class TareaController : Controller
{
    private TareaRepository tareaRepository;

    // Constructor para inicializar el repositorio
    public TareaController()
    {
        tareaRepository = new TareaRepository();
    }

    // Acción para listar tareas
    public IActionResult Index()
    {
        var tareas = tareaRepository.GetAll();
        return View(tareas);
    }

    // Acción para mostrar la página de creación de tareas
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
