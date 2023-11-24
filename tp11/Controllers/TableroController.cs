// TableroController.cs

using Microsoft.AspNetCore.Mvc;
using tp10.Repositorios;
using tp10;

public class TableroController : Controller
{
    private TableroRepository tableroRepository;

    // Constructor para inicializar el repositorio
    public TableroController()
    {
        tableroRepository = new TableroRepository();
    }

    // Acción para listar tableros
    public IActionResult Index()
    {
        var tableros = tableroRepository.GetAll();
        return View(tableros);
    }

    // Acción para mostrar la página de creación de tableros
    public IActionResult Crear()
    {
        return View();
    }

    // Acción para procesar la creación de tableros
    [HttpPost]
    public IActionResult Crear(Tablero tablero)
    {
        if (ModelState.IsValid)
        {
            // Validar el modelo antes de intentar guardarlo
            tableroRepository.Create(tablero);

            // Redirigir a la acción Index después de crear el tablero
            return RedirectToAction("Index");
        }

        // Si el modelo no es válido, vuelve a mostrar la vista de creación con errores
        return View(tablero);
    }

    // Acción para mostrar la página de modificación de tableros
    public IActionResult Modificar(int id)
    {
        var tablero = tableroRepository.Get(id);
        return View(tablero);
    }

    // Acción para procesar la modificación de tableros
    [HttpPost]
    public IActionResult Modificar(Tablero tablero)
    {
        tableroRepository.Update(tablero.Id, tablero);
        return RedirectToAction("Index");
    }

    // Acción para eliminar tableros
    public IActionResult Eliminar(int id)
    {
        tableroRepository.Remove(id);
        return RedirectToAction("Index");
    }
}
