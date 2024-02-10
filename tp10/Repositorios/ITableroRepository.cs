using tp10.Models;

namespace tp10.Repositorios{
    public interface ITableroRepository{
        public void Create(Tablero tablero);
        public void Update(int id, Tablero tablero);
        public List<Tablero> GetAll();
        public Tablero Get(int id);
        public List<Tablero> GetByUser(int idUsuario);
        public void Remove(int id);
    }
}