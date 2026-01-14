using System.Data;
using Microsoft.Data.SqlClient;
using RestauranteAPI.Models;

namespace RestauranteAPI.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string _connectionString;

        public UsuarioRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("RestauranteDB") ?? "Not found";
        }

        public async Task<Usuario?> GetByEmailAsync(string email)
        {
            Usuario? usuario = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Nombre, Email, Password, Rol FROM Usuario WHERE Email = @Email";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            usuario = new Usuario
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Email = reader.GetString(2),
                                Password = reader.GetString(3),
                                Rol = reader.GetString(4)
                            };
                        }
                    }
                }
            }

            return usuario;
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            Usuario? usuario = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, Nombre, Email, Password, Rol FROM Usuario WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            usuario = new Usuario
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Email = reader.GetString(2),
                                Password = reader.GetString(3),
                                Rol = reader.GetString(4)
                            };
                        }
                    }
                }
            }

            return usuario;
        }

        public async Task AddAsync(Usuario usuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Usuario (Nombre, Email, Password, Rol) VALUES (@Nombre, @Email, @Password, @Rol)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@Email", usuario.Email);
                    command.Parameters.AddWithValue("@Password", usuario.Password);
                    command.Parameters.AddWithValue("@Rol", usuario.Rol);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
