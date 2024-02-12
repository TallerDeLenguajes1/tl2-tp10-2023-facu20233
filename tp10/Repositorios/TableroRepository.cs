using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using tp10.Models;
// dotnet add package System.Data.SQLite
namespace tp10.Repositorios
{
    public class TableroRepository : ITableroRepository
    {

        private readonly string cadenaConexion;
        public TableroRepository(string CadenaDeConexion)
        {
            this.cadenaConexion = CadenaDeConexion;
        }

        public List<Tablero> GetAll()
        {
            var queryString = @"SELECT * FROM Tablero;";
            List<Tablero> tableros = new List<Tablero>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tablero = new Tablero();

                        tablero.Id = Convert.ToInt32(reader["id"]);
                        tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                        tablero.Nombre = reader["nombre"].ToString();
                        tablero.Descripcion = reader["descripcion"].ToString();

                        tableros.Add(tablero);
                    }
                }
                connection.Close();
            }
            return tableros;
        }

        public void Update(int id, Tablero tablero)
        {
            var query = $"UPDATE Tablero SET id_usuario_propietario = @id_usuario_propietario, nombre = @nombre, descripcion = @descripcion WHERE id = @id";

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@id_usuario_propietario", tablero.IdUsuarioPropietario));
                command.Parameters.Add(new SQLiteParameter("@nombre", tablero.Nombre));
                command.Parameters.Add(new SQLiteParameter("@descripcion", tablero.Descripcion));
                command.Parameters.Add(new SQLiteParameter("@id", id));

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void Remove(int id)
        {
            var queryString = "DELETE FROM Tablero WHERE id = @id";

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);

                command.Parameters.Add(new SQLiteParameter("@id", id));

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public Tablero Get(int id)
        {
            var queryString = "SELECT * FROM Tablero WHERE id = @idTablero";

            var tablero = new Tablero();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@idTablero", id));
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tablero.Id = Convert.ToInt32(reader["id"]);
                        tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                        tablero.Nombre = reader["nombre"].ToString();
                        tablero.Descripcion = reader["descripcion"].ToString();
                    }
                }
                connection.Close();
            }

            return (tablero);
        }

        public void Create(Tablero tablero)
        {
            var query = $"INSERT INTO Tablero (id_usuario_propietario, nombre, descripcion) VALUES (@id_usuario_propietario, @nombre, @descripcion)";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {

                connection.Open();
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@id_usuario_propietario", tablero.IdUsuarioPropietario));
                command.Parameters.Add(new SQLiteParameter("@nombre", tablero.Nombre));
                command.Parameters.Add(new SQLiteParameter("@descripcion", tablero.Descripcion));

                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        // public List<Tarea> ObtenerTareasAsociadasAlTablero(int idTablero)
        // {
        //     var queryString = "SELECT * FROM Tarea WHERE id_tablero = @idTablero";

        //     List<Tarea> tareas = new List<Tarea>();

        //     using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        //     {
        //         connection.Open();
        //         SQLiteCommand command = new SQLiteCommand(queryString, connection);
        //         command.Parameters.Add(new SQLiteParameter("@idTablero", idTablero));

        //         using (SQLiteDataReader reader = command.ExecuteReader())
        //         {
        //             while (reader.Read())
        //             {
        //                 var tarea = new Tarea
        //                 {
        //                     Id = Convert.ToInt32(reader["id"]),
        //                     IdTablero = Convert.ToInt32(reader["id_tablero"]),
        //                     Nombre = reader["nombre"].ToString(),
        //                     Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]),
        //                     Descripcion = reader["descripcion"].ToString(),
        //                     Color = reader["color"].ToString(),
        //                     IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"])
        //                 };

        //                 tareas.Add(tarea);
        //             }
        //         }

        //         connection.Close();
        //     }

        //     return tareas;
        // }




        // public List<Tablero> GetByUser(int idUsuario)
        // {
        //     var queryString = "SELECT * FROM Tablero WHERE id_usuario_propietario = @idUser";

        //     var tableros = new List<Tablero>();
        //     using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        //     {
        //         connection.Open();
        //         SQLiteCommand command = new SQLiteCommand(queryString, connection);
        //         command.Parameters.Add(new SQLiteParameter("@idUser", idUsuario));
        //         using (SQLiteDataReader reader = command.ExecuteReader())
        //         {
        //             while (reader.Read())
        //             {
        //                 var tablero = new Tablero();
        //                 tablero.Id = Convert.ToInt32(reader["id"]);
        //                 tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
        //                 tablero.Nombre = reader["nombre"].ToString();
        //                 tablero.Descripcion = reader["descripcion"].ToString();
        //                 tableros.Add(tablero);
        //             }
        //         }
        //         connection.Close();
        //     }

        //     return (tableros);
        // }

    }
}