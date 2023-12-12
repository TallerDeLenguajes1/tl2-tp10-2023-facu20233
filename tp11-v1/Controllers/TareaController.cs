using Microsoft.AspNetCore.Mvc;
using tp10.Repositorios;
using tp10.ViewModels;  // Asegúrate de que tengas la referencia correcta al espacio de nombres de ViewModels
using tp10.Models;
using tp10.ViewModels;

public class TareaController : Controller
{
    private readonly TareaRepository tareaRepository;
    private readonly TableroRepository tableroRepository;  // Agregado para obtener información relacionada con tableros

    // Constructor para inicializar los repositorios
    public TareaController()
    {
        tareaRepository = new TareaRepository();
        tableroRepository = new TableroRepository();
    }

    // Acción para listar tareas
    public IActionResult Index()
    {
        var tareas = tareaRepository.GetAll();
        return View(tareas);
    }

    // Acción para mostrar la página de creación de tareas
    // Acción para mostrar la página de creación de tareas
    public IActionResult Crear()
    {
        var tableros = tableroRepository.GetAll();
        var viewModel = new CrearTareaViewModel { Tableros = tableros };
        return View(viewModel);
    }

    // Acción para procesar la creación de tareas
    [HttpPost]
    public IActionResult Crear(CrearTareaViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            // Validar el modelo antes de intentar guardarlo
            tareaRepository.Create(viewModel.IdTablero, viewModel.Tarea);

            // Redirigir a la acción Index después de crear la tarea
            return RedirectToAction("Index");
        }

        // Si el modelo no es válido, vuelve a mostrar la vista de creación con errores
        viewModel.Tableros = tableroRepository.GetAll();  // Asegúrate de volver a cargar la lista de tableros
        return View(viewModel);
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
