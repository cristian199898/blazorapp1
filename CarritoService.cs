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
    public class CarritoService : ICarritoService
    {
        private readonly SqlConnectionConfiguration _configuration;

        public CarritoService()
        {
        }

        public CarritoService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> CarritoInsert(Carrito carrito)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("Idcarrito", carrito.Idcarrito, DbType.Int32);
                parameters.Add("Cantidad", carrito.Cantidad, DbType.Int32);
                parameters.Add("Idproducto", carrito.Idproducto, DbType.Int32);
                parameters.Add("AgregarProducto", carrito.AgregarProducto, DbType.Int32);

                const string query = @"INSERT INTO Categoria (Idcarrito, Cantidad, Idproducto,AgregarProducto,) VALUES (@Idcarrito, @Cantidad, @Idproducto,@AgregarProducto,)";
                await conn.ExecuteAsync(query, new { carrito.Idcarrito, carrito.Cantidad, carrito.Idproducto, carrito.AgregarProducto }, commandType: CommandType.Text);
            }

            return true;
        }

        public async Task<Carrito> CarritoSelect(int id)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var query = @"SELECT IdCarrito, Cantidad, Idproducto, AgregarProducto
                            FROM Carrito
                            WHERE IdCarrito = @IdCarrito";
                return await conn.QueryFirstOrDefaultAsync<Carrito>(query.ToString(), new { IdCarrito = id }, commandType: CommandType.Text);
            }
        }

        public async Task<IEnumerable<Carrito>> GetAllCarrito()
        {
            IEnumerable<Carrito> carrito;

            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string query = "SELECT Carrito,Cantidad, Idproducto, AgregarProducto FROM Carrito";
                carrito = await conn.QueryAsync<Carrito>(query, commandType: CommandType.Text);
            }

            return carrito;
        }
    }
}

