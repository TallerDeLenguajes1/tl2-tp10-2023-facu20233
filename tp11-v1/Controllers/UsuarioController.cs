// UsuarioControler.cs
using Microsoft.AspNetCore.Mvc;
using tp10.Repositorios;
using tp10.ViewModels; // Asegúrate de tener la referencia correcta al espacio de nombres de los ViewModels
using tp10.Models;

public class UsuariosController : Controller
{
    private UsuarioRepository usuarioRepository;

    public UsuariosController()
    {
        usuarioRepository = new UsuarioRepository();
    }

    public IActionResult Index()
    {
        var usuarios = usuarioRepository.GetAll();
        return View(usuarios);
    }

    public IActionResult Crear()
    {
        var viewModel = new CrearUsuarioViewModel(); // Utiliza el ViewModel correspondiente
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Crear(CrearUsuarioViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var usuario = new Usuario
            {
                NombreDeUsuario = viewModel.NombreDeUsuario,
                Contrasenia1 = viewModel.Contrasenia,
                // Asigna otros campos según sea necesario
            };

            usuarioRepository.Create(usuario);

            return RedirectToAction("Index");
        }

        return View(viewModel);
    }

    // public IActionResult Modificar(int id)
    // {
    //     var usuario = usuarioRepository.Get(id);
    //     var viewModel = new ModificarUsuarioViewModel
    //     {
    //         Id = usuario.Id,
    //         NombreDeUsuario = usuario.NombreDeUsuario,
    //         // Asigna otros campos según sea necesario
    //     };

    //     return View(viewModel);
    // }

    [HttpPost]
    public IActionResult Modificar(Usuario usuario)
    {
        var existingUser = usuarioRepository.Get(usuario.Id);
        if (existingUser == null)
        {
            // Manejar el caso en que el usuario no existe
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            usuarioRepository.Update(usuario.Id, usuario);
            return RedirectToAction("Index");
        }

        // Si el modelo no es válido, vuelve a mostrar la vista de modificación con errores
        return View(usuario);
    }


    [HttpPost]
    public IActionResult Modificar(ModificarUsuarioViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var usuario = new Usuario
            {
                Id = viewModel.Id,
                NombreDeUsuario = viewModel.NombreDeUsuario,
                // Asigna otros campos según sea necesario
            };

            usuarioRepository.Update(usuario.Id, usuario);

            return RedirectToAction("Index");
        }

        return View(viewModel);
    }

    public IActionResult Eliminar(int id)
    {
        usuarioRepository.Remove(id);
        return RedirectToAction("Index");
    }
}
