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
    private readonly ITareaRepository _tareaRepository;


    public TableroController(ILogger<TableroController> logger, ITableroRepository tableroRepository, IUsuarioRepository usuarioRepository, ITareaRepository tareaRepository)
    {
        _logger = logger;
        _tableroRepository = tableroRepository;
        _usuarioRepository = usuarioRepository;
        _tareaRepository = tareaRepository;
    }
    
    

    // Acción para listar tableros
    public IActionResult Index()
    {
        try
        {
            if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });

            var nombreUsuario = HttpContext.Session.GetString("Usuario");
            var usuario = _usuarioRepository.GetNombre(nombreUsuario).Id;

            var usuarios = _usuarioRepository.GetAll();
            var tablerosPropios = _tableroRepository.GetByUser(usuario);

            if (esAdmin())
            {
                ViewBag.AdminMessage = $"¡Logueado como {nombreUsuario}!";
                var todosTableros = _tableroRepository.GetAll();
                var tablerosOtros = _tableroRepository.GetTableroTareasAsignadas(usuario); 

                var viewModel = new ListarTablerosViewModel(tablerosPropios, tablerosOtros, usuarios, todosTableros){
                    EsAdmin = true
                };
                return View(viewModel);
            }
            else
            {
                ViewBag.AdminMessage = $"¡Logueado como {nombreUsuario}!";
                // var tableros = _tableroRepository.GetByUser(usuario);
                var tablerosOtros = _tableroRepository.GetTableroTareasAsignadas(usuario); 

                var viewModel = new ListarTablerosViewModel(tablerosPropios, tablerosOtros, usuarios){
                    EsAdmin = false
                };
                return View(viewModel);
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

    [HttpGet]
    public IActionResult UpdateTablero(int id)
    {
        try
        {
            if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });

            var usuarios = _usuarioRepository.GetAll();
            var userId = HttpContext.Session.GetInt32("Id") ?? 0;
            var user = _usuarioRepository.Get(userId);

            var viewModel = new ModificarTableroViewModel(_tableroRepository.Get(id),usuarios){
                IdUsuarioPropietario = user.Id,
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
    public IActionResult UpdateTablero(ModificarTableroViewModel tablero)
    {
        try
        {
            if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
            if (!ModelState.IsValid) return RedirectToAction("Index");

            _tableroRepository.Update(tablero.Id, new Tablero(tablero));
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    public IActionResult DeleteTablero(int id)
    {
        try
        {
            if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
            if (!ModelState.IsValid) return RedirectToAction("Index", id);

            // var idTablero = _tableroRepository.Get(id).Id;
            var tareasAsociadas = _tableroRepository.ObtenerTareasAsociadasAlTablero(id);

            foreach (var tarea in tareasAsociadas)
            {
                _tareaRepository.Delete(tarea.Id);
            }

            _tableroRepository.Remove(id);
            return RedirectToAction("Index");
            // return RedirectToAction("Index", new { id = idTablero });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpGet]
    public IActionResult CrearTablero()
    {
        try
        {
            if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });

            var usuarios = _usuarioRepository.GetAll();

            var viewModel = new CrearTableroViewModel
            {

                ListaUsuarios = usuarios,
            };

            // var viewModel = new CrearTableroViewModel();
            return View(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpPost]
    public IActionResult CrearTablero(CrearTableroViewModel tablero)
    {
        try
        {
            if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
            if (!ModelState.IsValid) return RedirectToAction("Index");


            _tableroRepository.Create(new Tablero(tablero)); //*
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpGet]
    public IActionResult AgregarTablero()
    {
        try
        {
            if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });

            var nombreUsuario = HttpContext.Session.GetString("Usuario");
            var usuario = _usuarioRepository.GetNombre(nombreUsuario);

            var viewModel = new CrearTableroViewModel
            {
                IdUsuarioPropietario = usuario.Id
            };
            // var viewModel = new CrearTableroViewModel();

            return View(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpPost]
    public IActionResult AgregarTablero(CrearTableroViewModel tablero)
    {
        try
        {
            if (!logueado()) return RedirectToRoute(new { controller = "Login", action = "Index" });
            if (!ModelState.IsValid) return RedirectToAction("Index");

            _tableroRepository.Agregar(tablero.IdUsuarioPropietario, new Tablero(tablero)); //*
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }




    // -------- controles ------------

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
