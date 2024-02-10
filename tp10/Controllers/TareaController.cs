// // TareaController.cs

// using Microsoft.AspNetCore.Mvc;
// using tp10.Repositorios;
// using tp10.Models;
// using tp10.ViewModels;
// namespace tp10.Controllers;

// public class TareaController : Controller
// {
//     private TareaRepository tareaRepository;
//     private ManejoController manejoController;
//     private TableroRepository tableroRepository;

//     // Constructor para inicializar el repositorio
//     public TareaController()
//     {
//         tareaRepository = new TareaRepository();
//         manejoController = new ManejoController();
//         tableroRepository = new TableroRepository();
//     }

//     // Acción para listar tareas
//     public IActionResult Index()
//     {
//         if (!manejoController.IsLogged(HttpContext)) return RedirectToAction("Index");
//         var tareas = tareaRepository.GetAll();
//         return View(tareas);
//     }

//     // public IActionResult Tareas(int id)
//     // {
//     //     if (!manejoController.IsLogged(HttpContext)) return RedirectToAction("Index");

//     //     var tareas = tareaRepository.GetTareasPorTablero(id);

//     //     return View(tareas);
//     // }

//     // Acción para mostrar la página de creación de tareas

//     public IActionResult TareasAsociadas(int id)
//     {
//         if (!manejoController.IsLogged(HttpContext)) return RedirectToAction("Index");

//         var tablero = tableroRepository.Get(id);

//         if (tablero == null)
//         {
//             return NotFound(); // Devolver un error 404 si el tablero no se encuentra
//         }

//         var tareasAsociadas = tareaRepository.ObtenerTareasAsociadasAlTablero(id); // 
//         return View(tareasAsociadas);
//     }

//     public IActionResult Crear()
//     {
//         if (!manejoController.IsLogged(HttpContext)) return RedirectToAction("Index");
//         var TareaVM = new CrearTareaViewModel();
//         return View(TareaVM);
//     }

//     // Acción para procesar la creación de tareas
//     [HttpPost]
//     public IActionResult Crear(CrearTareaViewModel TareaVM)
//     {
//         if (!manejoController.IsLogged(HttpContext)) return RedirectToAction("Index");
//         if (!ModelState.IsValid) return RedirectToAction("Index");

//         tareaRepository.Create(TareaVM.IdTablero, new Tarea(TareaVM));

//         return RedirectToAction("TareasAsociadas", new { idTablero = TareaVM.IdTablero });
//     }

//     [HttpGet]
//     // Acción para mostrar la página de modificación de tareas
//     public IActionResult Modificar(int id)
//     {
//         if (!manejoController.IsLogged(HttpContext)) return RedirectToAction("Index");
//         if (!manejoController.IsAdmin(HttpContext)) return RedirectToAction("Index");

//         var tareaVM = new ModificarTareaViewModel(tareaRepository.Get(id));
//         return View(tareaVM);
//     }

//     // Acción para procesar la modificación de tareas
//     [HttpPost]
//     public IActionResult Modificar(ModificarTareaViewModel tareaVM)
//     {
//         if (!manejoController.IsLogged(HttpContext)) return RedirectToAction("Index");
//         if (!ModelState.IsValid) return RedirectToAction("Index");

//         tareaRepository.Update(tareaVM.Id, new Tarea(tareaVM));
//         return RedirectToAction("TareasAsociadas", new { idTablero = tareaVM.IdTablero });
//     }

//     // Acción para eliminar tareas
//     public IActionResult Eliminar(int id)
//     {
//         tareaRepository.Remove(id);
//         return RedirectToAction("Index");
//     }
// }
