// UsuariosController.cs

using Microsoft.AspNetCore.Mvc;
using tp10.Repositorios; // Asegúrate de que tengas la referencia correcta al espacio de nombres del repositorio
using tp10.Models;
using tp10.ViewModels;
namespace tp10.Controllers;
public class UsuariosController : Controller
{
    private UsuarioRepository usuarioRepository;
    private ManejoController manejoController;

    // Constructor para inicializar el repositorio
    public UsuariosController()
    {
        usuarioRepository = new UsuarioRepository();
        manejoController = new ManejoController();
    }

    // Acción para listar usuarios

    public IActionResult Index()
    {
        if (!manejoController.IsLogged(HttpContext)) return RedirectToAction("Index");
        var usuarios = usuarioRepository.GetAll();

        return View(usuarios);
    }

    //     public IActionResult Index()
    // {
    //     if (!manejoController.IsLogged(HttpContext))
    //     {
    //         return RedirectToAction("Index");
    //     }

    //     var usuarios = usuarioRepository.GetAll();
    //     var usuariosViewModel = new ListarUsuariosViewModel
    //     {
    //         Usuarios = usuarios.Select(u => new UsuarioViewModel
    //         {
    //             Id = u.Id,
    //             NombreDeUsuario = u.NombreDeUsuario,
    //             // Añade otras propiedades según sea necesarizo
    //         }).ToList()
    //     };

    //     return View(usuariosViewModel);
    // }

    // Acción para mostrar la página de creación de usuarios, 
    [HttpGet]
    public IActionResult Crear() // metodo, view
    {
        if (!manejoController.IsLogged(HttpContext)) return RedirectToAction("Index");
        if (!manejoController.IsAdmin(HttpContext)) return RedirectToAction("Index");
        return View(new CrearUsuarioViewModel());
    }

    // Acción para procesar la creación de usuarios
    [HttpPost]
    public IActionResult Crear(CrearUsuarioViewModel usuario)
    {
        if (!manejoController.IsLogged(HttpContext)) return RedirectToAction("Index");
        if (!ModelState.IsValid) return RedirectToAction("CreateUser");

        usuarioRepository.Create(new Usuario(usuario));

        // Redirigir a la acción Index después de crear el usuario
        return RedirectToAction("Index");

    }

    // Acción para mostrar la página de modificación de usuarios
    [HttpGet]
    public IActionResult Modificar(int idUsuario)
    {
        if (!manejoController.IsLogged(HttpContext)) return RedirectToAction("Index");
        if (!manejoController.IsAdmin(HttpContext)) return RedirectToAction("Index");

        var usuario = usuarioRepository.Get(idUsuario);

        var usuarioVM = new ModificarUsuarioViewModel(usuario.Id);

        return View(usuarioVM);
    }

    // Acción para procesar la modificación de usuarios
    [HttpPost]
    public IActionResult Modificar(ModificarUsuarioViewModel usuarioVM)
    {
        if (!manejoController.IsLogged(HttpContext)) return RedirectToAction("Index");
        if (!ModelState.IsValid) return RedirectToAction("index");

        usuarioRepository.Update(usuarioVM.Id, new Usuario(usuarioVM));

        return RedirectToAction("Index");
    }

    // Acción para eliminar usuarios
    public IActionResult Eliminar(int id)
    {
        if (!manejoController.IsLogged(HttpContext)) return RedirectToAction("Index");
        if (!manejoController.IsAdmin(HttpContext)) return RedirectToAction("Index");
        usuarioRepository.Remove(id);
        return RedirectToAction("Index");
    }
}