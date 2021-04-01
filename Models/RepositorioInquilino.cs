using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Alvarez_Inmobiliaria.Models
{
    public class RepositorioInquilino : RepositorioBase
    {
       public RepositorioInquilino(IConfiguration configuration) : base(configuration)
        {

        }

		public IList<Inquilino> ObtenerTodos()
		{
			IList<Inquilino> res = new List<Inquilino>();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"SELECT IdInquilino, Dni, Apellido, Nombre, Direccion, Telefono" +
					$" FROM Inquilino";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						Inquilino p = new Inquilino
						{
							IdInquilino = reader.GetInt32(0),
							Dni = reader.GetString(1),
							Apellido = reader.GetString(2),
							Nombre = reader.GetString(3),
							Direccion = reader.GetString(4),
							Telefono = reader.GetString(5),
						};
						res.Add(p);
					}
					connection.Close();
				}
			}
			return res;
		}

		public Inquilino ObtenerPorId(int id)
		{

			Inquilino e = null;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"SELECT IdInquilino, Dni, Apellido, Nombre, Direccion, Telefono FROM Inquilino" +
					$" WHERE IdInquilino=@id;";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@id", id);
					connection.Open();
					SqlDataReader reader = command.ExecuteReader();
					if (reader.Read())
					{
						e = new Inquilino
						{
							IdInquilino = reader.GetInt32(0),
							Dni = reader.GetString(1),
							Apellido = reader.GetString(2),
							Nombre = reader.GetString(3),
							Direccion = reader.GetString(4),
							Telefono = reader.GetString(5)
						};
					}
					connection.Close();
				}
			}
			return e;
		}

		public int Alta(Inquilino e)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"INSERT INTO Inquilino(Dni, Apellido, Nombre, Direccion, Telefono) " +
                $"VALUES ('{e.Dni}', '{e.Apellido}', '{e.Nombre}', '{e.Direccion}', '{e.Telefono}')";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    command.CommandText = "SELECT SCOPE_IDENTITY()";
                    var id = command.ExecuteScalar();
                    e.IdInquilino = Convert.ToInt32(id);
                    connection.Close();
                }
            }
            return res;
        }
		/*public int Modificacion(Inquilino e)
		{
			int res = -1;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"UPDATE Inquilino SET Dni='{e.Dni}', Apellido='{e.Apellido}', Nombre='{e.Nombre}', Direccion'{e.Direccion}', Telefono='{e.Telefono}' " +
					$"WHERE IdInquilino = {e.IdInquilino}";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}*/

		public int Modificacion(Inquilino e)
		{
			int res = -1;

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = "UPDATE Inquilino SET " +
					$"{nameof(Inquilino.Dni)}=@dni, " +
					$"{nameof(Inquilino.Apellido)}=@apellido, " +
					$"{nameof(Inquilino.Nombre)}=@nombre, " +
					$"{nameof(Inquilino.Direccion)}=@direccion, " +
					$"{nameof(Inquilino.Telefono)}=@telefono " +
					$"WHERE {nameof(Inquilino.IdInquilino)}=@id;";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@dni", e.Dni);
					command.Parameters.AddWithValue("@apellido", e.Apellido);
					command.Parameters.AddWithValue("@nombre", e.Nombre);
					command.Parameters.AddWithValue("@direccion", e.Direccion);
					command.Parameters.AddWithValue("@telefono", e.Telefono);
					command.Parameters.AddWithValue("@id", e.IdInquilino);
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}
		public int Baja(int id)
		{
			int res = -1;

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"DELETE FROM Inquilino WHERE {nameof(Inquilino.IdInquilino)} = @id;";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@id", id);
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}

			}
			return res;
		}
	}
}
