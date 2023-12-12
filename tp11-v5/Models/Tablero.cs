// tablero.cs
using System.Collections.Generic;
using tp10.ViewModels;
namespace tp10.Models;

public class Tablero{
    private int id;
    private int idUsuarioPropietario;
    private string nombre;
    private string descripcion;
    public List<Tarea> Tareas { get; set; } = new List<Tarea>();

    public int Id { get => id; set => id = value; }
    public int IdUsuarioPropietario { get => idUsuarioPropietario; set => idUsuarioPropietario = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }

    public Tablero()
    {
    }

    public Tablero(CrearTableroViewModel CreTableroVM)
    {
        IdUsuarioPropietario = CreTableroVM.IdUsuarioPropietario;
        Nombre = CreTableroVM.Nombre;
        Descripcion = CreTableroVM.Descripcion;
    }

    public Tablero(ModificarTableroViewModel tablero){
        this.Id = tablero.Id;
        this.IdUsuarioPropietario = tablero.IdUsuarioPropietario;
        this.Nombre = tablero.Nombre;
        this.Descripcion = tablero.Descripcion;
    }
}