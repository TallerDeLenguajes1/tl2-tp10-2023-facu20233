using tp10.Models;

namespace tp10.Repositorios
{
    public interface ITareaRepository
    {

        public List<Tarea> GetAll();
        public void Update(int id, Tarea tarea);
        public Tarea Get(int id);

        public void Delete(int id);

        public void Create(Tarea tarea);

        public void CreateEnTablero(int idT, Tarea tarea);

        public List<Tarea> GetByUser(int idUsuario);

    }
}

