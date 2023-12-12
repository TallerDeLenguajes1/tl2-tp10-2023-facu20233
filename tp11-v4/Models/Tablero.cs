// tablero.cs
using System.Collections.Generic;
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
    
}