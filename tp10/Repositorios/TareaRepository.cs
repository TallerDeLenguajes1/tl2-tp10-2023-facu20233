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
        private readonly string cadenaConexion;

        public TareaRepository(string CadenaDeConexion)
        {
            this.cadenaConexion = CadenaDeConexion;
        }

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
            if(tareas == null) throw new Exception("Hubo un problema al buscar las tareas");
            return tareas;
        }

        public void Update(int id, Tarea tarea)
        {
            var query = $"UPDATE Tarea SET id_tablero = @id_tablero, nombre = @nombre, estado = @estado, descripcion = @descripcion, color = @color, id_usuario_asignado = @id_usuario_asignado WHERE id = @id";

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@id", id));
                command.Parameters.Add(new SQLiteParameter("@id_tablero", tarea.IdTablero));
                command.Parameters.Add(new SQLiteParameter("@nombre", tarea.Nombre));
                command.Parameters.Add(new SQLiteParameter("@estado", tarea.Estado));
                command.Parameters.Add(new SQLiteParameter("@descripcion", tarea.Descripcion));
                command.Parameters.Add(new SQLiteParameter("@color", tarea.Color));
                command.Parameters.Add(new SQLiteParameter("@id_usuario_asignado", tarea.IdUsuarioAsignado));

                var filas = command.ExecuteNonQuery();
                connection.Close();

                if (filas == 0) throw new Exception("Hubo un problema al modificar la tarea");
            }
        }

        public Tarea Get(int id)
        {
            var queryString = "SELECT * FROM Tarea WHERE id = @id";

            Tarea tarea = null;
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@id", id));
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tarea = new Tarea();

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

            if (tarea == null) throw new Exception("No se encontro");
            return (tarea);
        }

        public List<Tarea> GetByUser(int idUsuario)
        {
            var queryString = "SELECT * FROM Tarea WHERE id_usuario_asignado = @id_usuario_asignado";

            List<Tarea> tareas = null;
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@id_usuario_asignado", idUsuario));
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    tareas = new List<Tarea>();
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
            if (tareas == null) throw new Exception("Hubo un problema al buscar");
            return tareas;
        }

        public void Delete(int id)
        {
            var queryString = "DELETE FROM Tarea WHERE id = @id";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();            
                SQLiteCommand command = new SQLiteCommand(queryString, connection);

                command.Parameters.Add(new SQLiteParameter("@id", id));

                var filas = command.ExecuteNonQuery();
                connection.Close();

                if(filas == 0) throw new Exception("Hubo un problema al eliminar la tarea especificada");
            }
        }

        public void Create(Tarea tarea)
        {
            var query = $"INSERT INTO Tarea (id_tablero, nombre, estado, descripcion, color, id_usuario_asignado) VALUES (@id_tablero, @nombre, @estado, @descripcion, @color, @id_usuario_asignado)";
    
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {

                connection.Open();
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@id_tablero", tarea.IdTablero));
                command.Parameters.Add(new SQLiteParameter("@nombre", tarea.Nombre));
                command.Parameters.Add(new SQLiteParameter("@estado", tarea.Estado));
                command.Parameters.Add(new SQLiteParameter("@descripcion", tarea.Descripcion));
                command.Parameters.Add(new SQLiteParameter("@color", tarea.Color));
                command.Parameters.Add(new SQLiteParameter("@id_usuario_asignado", tarea.IdUsuarioAsignado));

                var filas = command.ExecuteNonQuery();
                connection.Close();  

                if(filas == 0) throw new Exception("Hubo un problema al crear la tarea");
            }
        }

        public void CreateEnTablero(int idT, Tarea tarea)
        {
            var query = $"INSERT INTO Tarea (id_tablero, nombre, estado, descripcion, color, id_usuario_asignado) VALUES (@id_tablero, @nombre, @estado, @descripcion, @color, @id_usuario_asignado)";
    
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {

                connection.Open();
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@id_tablero", idT));
                command.Parameters.Add(new SQLiteParameter("@nombre", tarea.Nombre));
                command.Parameters.Add(new SQLiteParameter("@estado", tarea.Estado));
                command.Parameters.Add(new SQLiteParameter("@descripcion", tarea.Descripcion));
                command.Parameters.Add(new SQLiteParameter("@color", tarea.Color));
                command.Parameters.Add(new SQLiteParameter("@id_usuario_asignado", tarea.IdUsuarioAsignado));

                var filas = command.ExecuteNonQuery();
                connection.Close();  

                if(filas == 0) throw new Exception("Hubo un problema al crear la tarea");
            }
        }
    }

}