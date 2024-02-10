using tp10.Models;

namespace tp10.Repositorios
{
    public interface IUsuarioRepository
    {
        public void Create(Usuario usuario);
        public void Update(int id, Usuario usuario);
        public List<Usuario> GetAll();
        public Usuario Get(int id);
        public void Remove(int id);
        public Usuario GetUsuario(string nombreDeUsuario, string contrasenia);

        public Usuario AutenticarUsuario(string nombreUsuario, string contrasenia);
    }
}