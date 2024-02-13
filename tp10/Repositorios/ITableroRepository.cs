using tp10.Models;

namespace tp10.Repositorios{
    public interface ITableroRepository{
        public List<Tablero> GetAll();
        public void Update(int id, Tablero tablero);
        public void Remove(int id);
        public Tablero Get(int id);
        public void Create(Tablero tablero);
        public List<Tarea> ObtenerTareasAsociadasAlTablero(int idTablero);

        public List<Tablero> GetByUser(int idUsuario);
    }
}