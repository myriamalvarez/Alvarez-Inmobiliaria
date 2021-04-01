using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Alvarez_Inmobiliaria.Models
{
	public class RepositorioPropietario : RepositorioBase
	{
		public RepositorioPropietario(IConfiguration configuration) : base(configuration)
		{

		}

		public IList<Propietario> ObtenerTodos()
		{
			IList<Propietario> res = new List<Propietario>();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"SELECT IdPropietario, Nombre, Apellido, Dni, Telefono,Email, Clave" +
					$" FROM Propietario";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						Propietario p = new Propietario
						{
							IdPropietario = reader.GetInt32(0),
							Nombre = reader.GetString(1),
							Apellido = reader.GetString(2),
							Dni = reader.GetString(3),
							Telefono = reader.GetString(4),
							Email = reader.GetString(5),
							Clave = reader.GetString(6)
							
						};
						res.Add(p);
					}
					connection.Close();
				}
			}
			return res;
		}
		public Propietario ObtenerPorId(int id)
		{
			
			Propietario p = null;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"SELECT IdPropietario, Nombre, Apellido, Dni, Telefono, Email, Clave FROM Propietario" +
					$" WHERE IdPropietario=@id;";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@id", id);
					connection.Open();
					SqlDataReader reader = command.ExecuteReader();
					if (reader.Read())
					{
						p = new Propietario
						{
							IdPropietario = reader.GetInt32(0),
							Nombre = reader.GetString(1),
							Apellido = reader.GetString(2),
							Dni = reader.GetString(3),
							Telefono = reader.GetString(4),
							Email = reader.GetString(5),
							Clave = reader.GetString(6),
						};
					}
					connection.Close();
				}
			}
			return p;
		}

		public int Alta(Propietario p)
		{
			int res = -1;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"INSERT INTO Propietario (Nombre, Apellido, Dni, Telefono, Email, Clave) " +
				$"VALUES ('{p.Nombre}', '{p.Apellido}', '{p.Dni}', '{p.Telefono}', '{p.Email}', '{p.Clave}')";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.CommandType = System.Data.CommandType.Text;
					connection.Open();
					res = command.ExecuteNonQuery();
					command.CommandText = "SELECT SCOPE_IDENTITY()";
					var id = command.ExecuteScalar();
					p.IdPropietario = Convert.ToInt32(id);
					connection.Close();
				}
			}
			return res;
		}

		/*public int Modificacion(Propietario p)
		{
			int res = -1;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"UPDATE Propietario SET Nombre='{p.Nombre}', Apellido='{p.Apellido}', Dni='{p.Dni}', Telefono='{p.Telefono}', Email='{p.Email}' " +
					$"WHERE IdPropietario = {p.IdPropietario}";
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
		/*public int Modificacion(Propietario p)
		{
			int res = -1;

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = "UPDATE Propietarios SET " +
					$"{nameof(Propietario.Nombre)}=@nombre, " +
					$"{nameof(Propietario.Apellido)}=@apellido, " +
					$"{nameof(Propietario.Dni)}=@dni, " +
					$"{nameof(Propietario.Telefono)}=@telefono, " +
					$"{nameof(Propietario.Email)}=@email, " +
					$"{nameof(Propietario.Clave)}=@clave " +
					$"WHERE {nameof(Propietario.IdPropietario)}=@id;";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@nombre", p.Nombre);
					command.Parameters.AddWithValue("@apellido", p.Apellido);
					command.Parameters.AddWithValue("@dni", p.Dni);
					command.Parameters.AddWithValue("@telefono", p.Telefono);
					command.Parameters.AddWithValue("@email", p.Email);
					command.Parameters.AddWithValue("@clave", p.Clave);
					command.Parameters.AddWithValue("@id", p.IdPropietario);
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}*/
		public int Modificacion(Propietario p)
		{
			int res = -1;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"UPDATE Propietario SET Nombre='{p.Nombre}', Apellido='{p.Apellido}', Dni='{p.Dni}', Telefono='{p.Telefono}', Email='{p.Email}' " +
					$"WHERE IdPropietario = {p.IdPropietario}";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
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
				string sql = $"DELETE FROM Propietario WHERE {nameof(Propietario.IdPropietario)} = @id;";
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
