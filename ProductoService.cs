using Dapper;
using Microsoft.Data.SqlClient;
 
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp1.Data.Model;
using BlazorApp1.Data;
 


namespace BlazorApp1.Data.Service
{
    public class ProductoService : IProductoService
    {
        private readonly SqlConnectionConfiguration _configuration;

        public ProductoService()
        {
        }

        public ProductoService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> ProductoInsert(Producto producto)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("IdProducto", producto.IdProducto, DbType.Int32);
                parameters.Add("NombreProducto", producto.NombreProducto, DbType.String);
                parameters.Add("Descripcion", producto.Descripcion, DbType.String);
                parameters.Add("Stock", producto.Stock, DbType.Int32);
                parameters.Add("Marca", producto.Marca, DbType.String);
                parameters.Add("Precio", producto.Precio, DbType.String);

                const string query = @"INSERT INTO Producto (IdProducto, NombreProducto, Descripcion , Stock ) VALUES (@IdProducto, @NombreProducto, @Descripcion, @Stock, @Marca, @Precio )";
                await conn.ExecuteAsync(query, new { producto.IdProducto, producto.NombreProducto, producto.Descripcion, producto.Stock, producto.Marca, producto.Precio }, commandType: CommandType.Text);
            }

            return true;
        }

        public async Task<Producto> ProductoSelect(int id)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var query = @"SELECT IdProducto, NombreProducto, ApellidoProducto
                            FROM Producto
                            WHERE IdProducto = @IdProducto";
                return await conn.QueryFirstOrDefaultAsync<Producto>(query.ToString(), new { IdProducto = id }, commandType: CommandType.Text);
            }
        }

        public async Task<IEnumerable<Producto>> GetAllProductos()
        {
            IEnumerable<Producto> productos;

            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string query = "SELECT IdProducto, NombreProducto, ApellidoProducto FROM Producto";
                productos = await conn.QueryAsync<Producto>(query, commandType: CommandType.Text);
            }

            return productos;
        }
    }
}