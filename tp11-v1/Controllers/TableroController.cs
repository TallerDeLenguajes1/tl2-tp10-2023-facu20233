using Microsoft.AspNetCore.Mvc;
using tp10.Repositorios;
using tp10.ViewModels;
using tp10.Models;

public class TableroController : Controller
{
    private readonly TableroRepository tableroRepository;

    public TableroController(TableroRepository tableroRepository)
    {
        this.tableroRepository = tableroRepository;
    }

    public IActionResult Index()
    {
        var tableros = tableroRepository.GetAll();
        return View(tableros);
    }

    public IActionResult ListarTodos()
    {
        // Verificar el rol del usuario (se recomienda utilizar políticas de autorización en lugar de directamente desde la sesión)
        var rolUsuario = HttpContext.User.Identity.IsAuthenticated
            ? (Rol)Enum.Parse(typeof(Rol), HttpContext.Session.GetString("NivelAcceso"))
            : Rol.operador;

        if (rolUsuario == Rol.administrador)
        {
            var tableros = tableroRepository.GetAll();
            return View(tableros);
        }

        var usuarioId = Convert.ToInt32(HttpContext.Session.GetString("UsuarioId"));
        var tablerosUsuario = tableroRepository.GetByUser(usuarioId);
        return View(tablerosUsuario);
    }

    public IActionResult Crear()
    {
        var viewModel = new CrearTableroViewModel();
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Crear(CrearTableroViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var tablero = new Tablero
            {
                Nombre = viewModel.Nombre,
                Descripcion = viewModel.Descripcion,
                IdUsuarioPropietario = 1, // Debes establecer el usuario propietario según tu lógica
            };

            tableroRepository.Create(tablero);

            return RedirectToAction("Index");
        }

        return View(viewModel);
    }

    public IActionResult Modificar(int id)
    {
        var tablero = tableroRepository.Get(id);
        var viewModel = new ModificarTableroViewModel
        {
            Id = tablero.Id,
            Nombre = tablero.Nombre,
            Descripcion = tablero.Descripcion,
        };

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Modificar(ModificarTableroViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var tablero = new Tablero
            {
                Id = viewModel.Id,
                Nombre = viewModel.Nombre,
                Descripcion = viewModel.Descripcion,
            };

            tableroRepository.Update(tablero.Id, tablero);

            return RedirectToAction("Index");
        }

        return View(viewModel);
    }

    public IActionResult Eliminar(int id)
    {
        tableroRepository.Remove(id);
        return RedirectToAction("Index");
    }
}
