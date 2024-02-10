using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using tp10.Models;
namespace tp10.ViewModels;

public class ListarTareasViewModel
{
    public List<TareaViewModel> Tareas { get; set; }
}