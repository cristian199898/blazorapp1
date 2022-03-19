using Dapper;
using Microsoft.Data.SqlClient;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp1.Data;
using BlazorApp1.Data.Model;


namespace BlazorApp1.Data.CrudService
{

    public class CrudService
    {
        private readonly SqlConnectionConfiguration _configuration;

        public CrudService()
        {
        }


        // INSERTAR PRODUCTO
        public async Task<bool> insert(Producto producto) // (INSERT)
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

        // TRAIGAME EL PRODUCTO MAS CARO
        public async Task<Producto> SelectMax(Producto producto) // 
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("Precio", producto.Precio, DbType.String);
                var query = @"SELECT 
                              FROM Producto 
                            
                            
                            WHERE Producto. 
                            ORDER BY Precio";
                return await conn.QueryFirstOrDefaultAsync<Producto>(query.ToString(), new { producto.Precio }, commandType: CommandType.Text);
            }
        }


        // TRAIGAME EL PRODUCTO menos CARO
        public async Task<Producto> SelectMin(Producto producto) // 
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("Precio", producto.Precio, DbType.String);
                var query = @"SELECT MIN(Precio)
                            FROM Producto
                            ORDER BY Precio";
                return await conn.QueryFirstOrDefaultAsync<Producto>(query.ToString(), new { producto.Precio }, commandType: CommandType.Text);
            }
        }

        // SELECCIONE LA ENTTREGA (READ)
        public async Task<Entrega> Select(int id)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                // var parameters = new DynamicParameters();
                // parameters.Add("IdCliente", id.IdProducto, DbType.Int32);
                var query = @"SELECT IdProducto, NombreProducto, Descripcion, Stock
                            FROM Producto
                            WHERE IdProducto = @IdProducto";
                return await conn.QueryFirstOrDefaultAsync<Entrega>(query.ToString(), new { IdProducto = id }, commandType: CommandType.Text);
            }
        }


        public async Task<bool> usuarioDeleteProducto(Producto producto) // (DELETE)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("IdProducto", producto.IdProducto, DbType.Int32);
                var query = @"DELETE FROM Producto
                            WHERE IdProducto = @IdProducto";
                await conn.ExecuteAsync(query.ToString(), new { producto.IdProducto }, commandType: CommandType.Text);
            }
            return true;
        }


        public async Task<bool> usuarioProductoUpdate(Producto producto) // (UPDATE)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("IdProducto", producto.IdProducto, DbType.Int32);
                parameters.Add("NombreProducto", producto.NombreProducto, DbType.String);
                parameters.Add("Precio", producto.Precio, DbType.Decimal);

                const string query = @"UPDATE Producto 
                    SET NombreProducto = @NombreProducto, 
                        PrecioProducto = @PrecioProducto,                        
                    WHERE IdProducto = @IdProducto";
                await conn.ExecuteAsync(query, new { producto.IdProducto, producto.NombreProducto, producto.Precio }, commandType: CommandType.Text);
            }
            return true;
        }
    }
}

