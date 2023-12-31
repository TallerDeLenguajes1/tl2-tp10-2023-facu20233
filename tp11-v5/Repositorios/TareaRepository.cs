using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using tp10.Models;
namespace tp10.Repositorios
{
    public class TareaRepository : ITareaRepository
    {
        private string cadenaConexion = "Data Source=db/kanban.db;Cache=Shared";
        public void Create(int idTablero, Tarea tarea)
        {
            var query = $"INSERT INTO Tarea (id_tablero, nombre, estado, descripcion, color, id_usuario_asignado) VALUES (@idTablero, @nombre, @estado, @descripcion, @color, @idUser)";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {

                connection.Open();
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@idTablero", idTablero));
                command.Parameters.Add(new SQLiteParameter("@nombre", tarea.Nombre));
                command.Parameters.Add(new SQLiteParameter("@estado", tarea.Estado));
                command.Parameters.Add(new SQLiteParameter("@descripcion", tarea.Descripcion));
                command.Parameters.Add(new SQLiteParameter("@color", tarea.Color));
                command.Parameters.Add(new SQLiteParameter("@idUser", tarea.IdUsuarioAsignado));

                command.ExecuteNonQuery();

                connection.Close();
            }
        }
        public void Update(int id, Tarea tarea)
        {
            var query = $"UPDATE Tarea SET id_tablero = @idTablero, nombre = @nombre, estado = @estado, descripcion = @descripcion, color = @color, id_usuario_asignado = @idUser WHERE id = @id";

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@id", id));
                command.Parameters.Add(new SQLiteParameter("@idTablero", tarea.IdTablero));
                command.Parameters.Add(new SQLiteParameter("@nombre", tarea.Nombre));
                command.Parameters.Add(new SQLiteParameter("@estado", tarea.Estado));
                command.Parameters.Add(new SQLiteParameter("@descripcion", tarea.Descripcion));
                command.Parameters.Add(new SQLiteParameter("@color", tarea.Color));
                command.Parameters.Add(new SQLiteParameter("@idUser", tarea.IdUsuarioAsignado));

                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public Tarea Get(int id)
        {
            var queryString = "SELECT * FROM Tarea WHERE id = @id";

            var tarea = new Tarea();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@id", id));
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tarea.Id = Convert.ToInt32(reader["id"]);
                        tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                        tarea.Descripcion = reader["descripcion"].ToString();
                        tarea.Color = reader["color"].ToString();
                        tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                    }
                }
                connection.Close();
            }

            return (tarea);
        }

        // --
        public List<Tarea> GetAll()
        {
            var queryString = "SELECT * FROM Tarea";

            var tareas = new List<Tarea>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
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
                            IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"])
                        };
                        tareas.Add(tarea);
                    }
                }
                connection.Close();
            }

            return tareas;
        }

        public List<Tarea> ObtenerTareasAsociadasAlTablero(int idTablero)
        {
            var queryString = "SELECT * FROM Tarea WHERE id_tablero = @idTablero";

            List<Tarea> tareas = new List<Tarea>();

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@idTablero", idTablero));

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
                            IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"])
                        };

                        tareas.Add(tarea);
                    }
                }

                connection.Close();
            }

            return tareas;
        }

        // public List<Tarea> GetTareasPorTablero(int idTablero)
        // {
        //     var queryString = "SELECT * FROM Tarea WHERE id_tablero = @idTablero";

        //     var tareas = new List<Tarea>();
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


        public List<Tarea> GetByTablero(int idTablero)
        {
            var queryString = "SELECT * FROM Tarea WHERE id_tablero = @idTablero";

            var tareas = new List<Tarea>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@idTablero", idTablero));
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tarea = new Tarea();
                        tarea.Id = Convert.ToInt32(reader["id"]);
                        tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                        tarea.Descripcion = reader["descripcion"].ToString();
                        tarea.Color = reader["color"].ToString();
                        tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                        tareas.Add(tarea);
                    }
                }
                connection.Close();
            }

            return (tareas);
        }
        public List<Tarea> GetByEstado(int estado)
        {
            var queryString = "SELECT * FROM Tarea WHERE estado = @estado";

            var tareas = new List<Tarea>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@estado", estado));
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tarea = new Tarea();
                        tarea.Id = Convert.ToInt32(reader["id"]);
                        tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                        tarea.Descripcion = reader["descripcion"].ToString();
                        tarea.Color = reader["color"].ToString();
                        tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                        tareas.Add(tarea);
                    }
                }
                connection.Close();
            }

            return (tareas);
        }
        public List<Tarea> GetByUser(int idUsuario)
        {
            var queryString = "SELECT * FROM Tarea WHERE id_usuario_asignado = @idUser";

            var tareas = new List<Tarea>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@idUser", idUsuario));
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tarea = new Tarea();
                        tarea.Id = Convert.ToInt32(reader["id"]);
                        tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                        tarea.Descripcion = reader["descripcion"].ToString();
                        tarea.Color = reader["color"].ToString();
                        tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                        tareas.Add(tarea);
                    }
                }
                connection.Close();
            }

            return (tareas);
        }
        public void Remove(int id)
        {
            var queryString = "DELETE FROM Tarea WHERE id = @id";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@id", id));
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void AsignarAUsuario(int idUsuario, int idTarea)
        {
            var query = $"UPDATE Tarea SET id_usuario_asignado = @idUser WHERE id = @idTarea";

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@idTarea", idTarea));
                command.Parameters.Add(new SQLiteParameter("@idUser", idUsuario));

                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}