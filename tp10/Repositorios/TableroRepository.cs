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
            if (tableros == null) throw new Exception("Hubo un problema al buscar los tableros");
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

                var filas = command.ExecuteNonQuery();
                connection.Close();

                if (filas == 0) throw new Exception("Hubo un problema al modificar el tablero");
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

                var filas = command.ExecuteNonQuery();
                connection.Close();

                if (filas == 0) throw new Exception("Hubo un problema al eliminar el tablero especificado");
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
            if (tablero == null) throw new Exception("No se encontro ningun tablero");
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

                var filas = command.ExecuteNonQuery();
                connection.Close();

                if (filas == 0) throw new Exception("Hubo un problema al crear el tablero");
            }
        }

        public void Agregar(int idUsuario, Tablero tablero)
        {
            var query = $"INSERT INTO Tablero (id_usuario_propietario, nombre, descripcion) VALUES (@id_usuario_propietario, @nombre, @descripcion)";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {

                connection.Open();
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@id_usuario_propietario", idUsuario));
                command.Parameters.Add(new SQLiteParameter("@nombre", tablero.Nombre));
                command.Parameters.Add(new SQLiteParameter("@descripcion", tablero.Descripcion));

                var filas = command.ExecuteNonQuery();
                connection.Close();

                if (filas == 0) throw new Exception("Hubo un problema al crear el tablero");
            }
        }

        public List<Tarea> ObtenerTareasAsociadasAlTablero(int idTablero)
        {
            var queryString = "SELECT * FROM Tarea WHERE id_tablero = @id_tablero";

            List<Tarea> tareas = new List<Tarea>();

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@id_tablero", idTablero));

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tarea = new Tarea
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            IdTablero = Convert.ToInt32(reader["id_tablero"]),
                            Nombre = reader["nombre"].ToString(),
                            Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]),
                            Descripcion = reader["descripcion"].ToString(),
                            Color = reader["color"].ToString(),
                            IdUsuarioAsignado = reader["id_usuario_asignado"] == DBNull.Value ? null : (int?)Convert.ToInt32(reader["id_usuario_asignado"])


                        };

                        tareas.Add(tarea);
                    }
                }

                connection.Close();
            }

            if (tareas == null)
                throw new Exception("No se encontro ningun tablero");
            return (tareas);
        }

        public List<Tablero> GetByUser(int idUsuario)
        {
            var queryString = @"SELECT * FROM Tablero WHERE id_usuario_propietario = @id_usuario_propietario;";

            List<Tablero> tableros = new List<Tablero>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@id_usuario_propietario", idUsuario)); //*
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
            if (tableros == null)
                throw new Exception("No se encontro ningun tablero");
            return (tableros);
        }


    }
}