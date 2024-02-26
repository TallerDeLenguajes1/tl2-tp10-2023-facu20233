using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using tp10.Models;
namespace tp10.ViewModels;

public class ModificarTareaViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Campo requerido")]
    public int IdTablero { get; set; }

    [Required(ErrorMessage = "El nombre de la tarea es obligatorio.")]
    [StringLength(30)]
    [Display(Name = "Nombre de la Tarea")]
    public string? Nombre { get; set; }

    [StringLength(50)]
    [Display(Name = "Descripción de la Tarea")]
    public string? Descripcion { get; set; }

    [Display(Name = "Estado de la Tarea")]
    public EstadoTarea Estado { get; set; }
    
    [StringLength(20)]
    [Display(Name = "Color de la Tarea")]
    public string? Color { get; set; }

    [Display(Name = "usuario_asignado de la Tarea")]
    public int? Id_usuario_asignado { get; set; }
    
    public List<Usuario>? ListaUsuarios { get; set; }

    public ModificarTareaViewModel()
    {
    }

    public ModificarTareaViewModel(Tarea tarea, List<Usuario> listaUsuarios)
    {
        Id = tarea.Id;
        IdTablero = tarea.IdTablero;
        Nombre = tarea.Nombre;
        Estado = tarea.Estado;
        Descripcion = tarea.Descripcion;
        Color = tarea.Color;
        Id_usuario_asignado = tarea.IdUsuarioAsignado;
        
        ListaUsuarios = listaUsuarios;
    }

}