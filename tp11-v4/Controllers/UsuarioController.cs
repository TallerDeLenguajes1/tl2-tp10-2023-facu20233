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
        /*
        // Obtener el rol del usuario actual
        var rol = User.IsInRole("admin") ? "admin" : User.IsInRole("operador") ? "operador" : "desconocido";

        // Puedes ahora pasar el rol a la vista o utilizarlo según sea necesario
        */

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


    // Acción para mostrar la página de creación de usuarios
    public IActionResult Crear()
    {
        if (!manejoController.IsAdmin(HttpContext)) return RedirectToAction("Index");
        return View();
    }

    // Acción para procesar la creación de usuarios
    [HttpPost]
    public IActionResult Crear(Usuario usuario)
    {
        if (ModelState.IsValid)
        {
            // Validar el modelo antes de intentar guardarlo

            usuarioRepository.Create(usuario);

            // Redirigir a la acción Index después de crear el usuario
            return RedirectToAction("Index");
        }

        // Si el modelo no es válido, vuelve a mostrar la vista de creación con errores
        return View(usuario);
    }

    // Acción para mostrar la página de modificación de usuarios
    public IActionResult Modificar(int id)
    {
        if (!manejoController.IsAdmin(HttpContext)) return RedirectToAction("Index");
        var usuario = usuarioRepository.Get(id);
        return View(usuario);
    }

    // Acción para procesar la modificación de usuarios
    [HttpPost]
    public IActionResult Modificar(Usuario usuario)
    {
        usuarioRepository.Update(usuario.Id, usuario);
        return RedirectToAction("Index");
    }

    // Acción para eliminar usuarios
    public IActionResult Eliminar(int id)
    {
        if (!manejoController.IsAdmin(HttpContext)) return RedirectToAction("Index");
        usuarioRepository.Remove(id);
        return RedirectToAction("Index");
    }
}