// Tarea.cs
using tp10.ViewModels;

namespace tp10.Models;


public enum EstadoTarea
{
    Ideas,
    ToDo,
    Doing,
    Review,
    Done
}

public class Tarea
{
    private int id;
    private int idTablero;
    private string? nombre;
    private string? descripcion;
    private string? color;
    private EstadoTarea estado;
    private int? idUsuarioAsignado;

    public int Id { get => id; set => id = value; }
    public int IdTablero { get => idTablero; set => idTablero = value; }
    public string? Nombre { get => nombre; set => nombre = value; }
    public string? Descripcion { get => descripcion; set => descripcion = value; }
    public string? Color { get => color; set => color = value; }
    public EstadoTarea Estado { get => estado; set => estado = value; }
    public int? IdUsuarioAsignado { get => idUsuarioAsignado; set => idUsuarioAsignado = value; }

    public Tarea()
    {
    }

    public Tarea(ModificarTareaViewModel upTareaVM)
    {
        IdTablero = upTareaVM.IdTablero;
        Nombre = upTareaVM.Nombre;
        Estado = upTareaVM.Estado;
        Descripcion = upTareaVM.Descripcion;
        Color = upTareaVM.Color;
        IdUsuarioAsignado = upTareaVM.Id_usuario_asignado;
    }

    public Tarea(CrearTareaViewModel creTareaVM)
    {
        idTablero = creTareaVM.IdTablero;
        Nombre = creTareaVM.Nombre;
        Estado = creTareaVM.Estado;
        Descripcion = creTareaVM.Descripcion;
        Color = creTareaVM.Color;
        idUsuarioAsignado = creTareaVM.IdUsuarioAsignado;
    }

}