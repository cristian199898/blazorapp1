using Dapper;
using Microsoft.Data.SqlClient;

//using System;
using System.Collections.Generic;
using System.Data;
//using System.Linq; 
using System.Threading.Tasks;
using BlazorApp1.Data;
using BlazorApp1.Data.Model;

namespace BlazorApp1.Data.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly SqlConnectionConfiguration _configuration;

        public UsuarioService()
        {
        }

        public UsuarioService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> UsuarioInsert(Usuario usuario)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("IdCliente", usuario.IdCliente, DbType.Int32);
                parameters.Add("Nombre", usuario.Nomble, DbType.String);
                parameters.Add("Apellido", usuario.Apellido, DbType.String);
                parameters.Add("Direccion", usuario.Direccion, DbType.String);
                parameters.Add("Barrio", usuario.Barrio, DbType.String);
                parameters.Add("Email", usuario.Email, DbType.String);
                parameters.Add("Celular", usuario.Celular, DbType.Double);


                const string query = @"INSERT INTO Usuario (IdCliente, Nombre, Apellido, Direccion, Barrio, Email, Celular) VALUES (@IdCliente, @Nombre, @Apellido, @Direccion, @Barrio, @Email, @Celular)";
                await conn.ExecuteAsync(query, new { usuario.IdCliente, usuario.Nomble, usuario.Apellido, usuario.Direccion, usuario.Barrio, usuario.Email, usuario.Celular }, commandType: CommandType.Text);
            }

            return true;
        }



        public async Task<bool> usuarioUpdate(Usuario usuario)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("IdCliente", usuario.IdCliente, DbType.Int32);
                parameters.Add("Nombre", usuario.Nomble, DbType.String);
                parameters.Add("Apellido", usuario.Apellido, DbType.String);
                parameters.Add("Direccion", usuario.Direccion, DbType.String);
                parameters.Add("Barrio", usuario.Barrio, DbType.String);
                parameters.Add("Email", usuario.Email, DbType.String);
                parameters.Add("Celular", usuario.Celular, DbType.Double);

                const string query = @"UPDATE Factura_Producto 
                    SET ValorProducto = @ValorProducto, 
                        CantidadProducto = @CantidadProducto
                    WHERE IdFactura=@IdFactura AND IdProducto = @IdProducto";
                await conn.ExecuteAsync(query, new { usuario.IdCliente, 
                    usuario.Nomble, 
                    usuario.Apellido, 
                    usuario.Direccion, 
                    usuario.Barrio, 
                    usuario.Email, 
                    usuario.Celular }, commandType: CommandType.Text);
            }
            return true;
        }

        public async Task<bool> usuarioDelete(Usuario usuario)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("IdFactura", usuario.IdCliente, DbType.Int32);

                var query = @"DELETE FROM Factura_Producto
                            WHERE IdFactura=@IdFactura AND IdProducto=@IdProducto";
                await conn.ExecuteAsync(query.ToString(), new { usuario.IdCliente }, commandType: CommandType.Text);
            }
            return true;
        }






        public async Task<Usuario> UsuarioSelect(int id)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var query = @"SELECT IdCliente, NombreCliente, ApellidoUsuario
                            FROM Usuario
                            WHERE IdCliente = @IdCliente";
                return await conn.QueryFirstOrDefaultAsync<Usuario>(query.ToString(), new { IdCliente = id }, commandType: CommandType.Text);
            }
        }

        public async Task<IEnumerable<Usuario>> GetAllUsuario()
        {
            IEnumerable<Usuario> usuarios;

            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string query = "SELECT IdCliente, Nombr, ApellidoUsuario FROM Usuario";
                usuarios = await conn.QueryAsync<Usuario>(query, commandType: CommandType.Text);
            }

            return usuarios;
        }
    }
}
