using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using tp10.Models;
namespace tp10.Repositorios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string cadenaConexion;

        public UsuarioRepository(string CadenaDeConexion)
        {
            this.cadenaConexion = CadenaDeConexion;
        }

        public Usuario AutenticarUsuario(string nombreUsuario, string contrasenia)
        {
            var queryString = "SELECT * FROM Usuario WHERE nombre_de_usuario = @nombre_de_usuario AND contrasenia = @contrasenia";

            Usuario usuario = null;
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@nombre_de_usuario", nombreUsuario));
                command.Parameters.Add(new SQLiteParameter("@contrasenia", contrasenia));
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        usuario = new Usuario();

                        usuario.Id = Convert.ToInt32(reader["id"]);
                        usuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                        usuario.Contrasenia = reader["contrasenia"].ToString();
                        usuario.Rol = (Rol)Convert.ToInt32(reader["rol"]);

                    }
                }
                connection.Close();
            }
            if (usuario == null) throw new Exception("No existe el usuario");

            return (usuario);
        }

        public List<Usuario> GetAll()
        {
            var queryString = @"SELECT * FROM Usuario;";

            List<Usuario> usuarios = new List<Usuario>();

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var usuario = new Usuario();

                        usuario.Id = Convert.ToInt32(reader["id"]);
                        usuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                        usuario.Contrasenia = reader["contrasenia"].ToString();
                        usuario.Rol = (Rol)Enum.Parse(typeof(Rol), reader["rol"].ToString());

                        usuarios.Add(usuario);
                    }
                }
                connection.Close();
            }
            if(usuarios == null) throw new Exception("Hubo un problema al buscar los tableros");
            return usuarios;
        }

        public void Update(int id, Usuario usuario)
        {
            var query = "UPDATE Usuario SET nombre_de_usuario = @nombre_de_usuario, contrasenia = @contrasenia, rol = @rol WHERE id = @id";

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@nombre_de_usuario", usuario.NombreDeUsuario));
                command.Parameters.Add(new SQLiteParameter("@contrasenia", usuario.Contrasenia)); // Agregar contraseña
                command.Parameters.Add(new SQLiteParameter("@rol", (int)usuario.Rol));
                command.Parameters.Add(new SQLiteParameter("@id", id));

                var filas = command.ExecuteNonQuery();
                connection.Close();

                if(filas == 0) throw new Exception("Hubo un problema al modificar el usuario");
            }
        }

        public Usuario Get(int id)
        {
            var queryString = "SELECT * FROM Usuario WHERE id = @id";

            var usuario = new Usuario();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@id", id));
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        usuario.Id = Convert.ToInt32(reader["id"]);
                        usuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                        usuario.Contrasenia = reader["contrasenia"].ToString();
                        usuario.Rol = (Rol)Enum.Parse(typeof(Rol), reader["rol"].ToString());
                    }
                }
                connection.Close();
            }

            if(usuario == null) throw new Exception("No se encontro ningun tablero");
            return (usuario);
        }

        public Usuario GetNombre(string nombreUsuario)
        {
            var queryString = "SELECT * FROM Usuario WHERE nombre_de_usuario = @nombre_de_usuario";

            var usuario = new Usuario();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@nombre_de_usuario", nombreUsuario)); //*
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        usuario.Id = Convert.ToInt32(reader["id"]);
                        usuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                        usuario.Rol = (Rol)Enum.Parse(typeof(Rol), reader["rol"].ToString());
                    }
                }
                connection.Close();
            }

            if(usuario == null) throw new Exception("No se encontro ningun tablero");
            return (usuario);
        }

        public void Delete(int id)
        {
            var queryString = "DELETE FROM Usuario WHERE id = @id";

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);

                command.Parameters.Add(new SQLiteParameter("@id", id));

                var filas = command.ExecuteNonQuery();
                connection.Close();

                if(filas == 0) throw new Exception("Hubo un problema al eliminar el usuario especificado");
            }
        }


        public void Create(Usuario usuario)
        {
            var query = @"INSERT INTO Usuario (nombre_de_usuario, contrasenia, rol) VALUES (@nombre_de_usuario, @contrasenia, @rol);";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {

                var command = new SQLiteCommand(query, connection);
                connection.Open();

                command.Parameters.Add(new SQLiteParameter("@nombre_de_usuario", usuario.NombreDeUsuario));
                command.Parameters.Add(new SQLiteParameter("@contrasenia", usuario.Contrasenia));
                command.Parameters.Add(new SQLiteParameter("@rol", (int)usuario.Rol));

                var filas = command.ExecuteNonQuery();
                connection.Close();  

                if(filas == 0) throw new Exception("Hubo un problema al crear el usuario");
            }
        }


        // public Usuario GetUsuario(string nombreDeUsuario, string contrasenia)
        // {
        //     var queryString = "SELECT * FROM Usuario WHERE nombre_de_usuario = @nombre AND contrasenia = @contrasenia";
        //     using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        //     {
        //         connection.Open();
        //         var command = new SQLiteCommand(queryString, connection);
        //         command.Parameters.Add(new SQLiteParameter("@nombre", nombreDeUsuario));
        //         command.Parameters.Add(new SQLiteParameter("@contrasenia", contrasenia));

        //         using (SQLiteDataReader reader = command.ExecuteReader())
        //         {
        //             while (reader.Read())
        //             {
        //                 return new Usuario
        //                 {
        //                     Id = Convert.ToInt32(reader["id"]),
        //                     NombreDeUsuario = reader["nombre_de_usuario"].ToString(),
        //                     Contrasenia = reader["contrasenia"].ToString(),
        //                     Rol = (Rol)Convert.ToInt32(reader["rol"])
        //                 };
        //             }
        //         }
        //     }

        //     return null;
        // }

    }
}