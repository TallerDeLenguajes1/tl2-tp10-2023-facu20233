using tp10.Models;

namespace tp10.Repositorios
{
    public interface IUsuarioRepository
    {
        public Usuario AutenticarUsuario(string nombreUsuario, string contrasenia);
        public List<Usuario> GetAll();
        public void Update(int id, Usuario usuario);
        public Usuario Get(int id);
        public Usuario GetNombre(string nombreUsuario);
        public void Delete(int id);
        public void Create(Usuario usuario);

        
    }
}