using Dapper;
using Microsoft.Data.SqlClient;
using BlazorApp1.Data.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Data.Service
{
    public class PedidoService : IPedidoService

    {
        private readonly SqlConnectionConfiguration _configuration;

        public PedidoService()
        {
        }

        public
PedidoService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool>
PedidoInsert(Pedido pedido)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("IdPedido", pedido.IdPeido, DbType.Int32);
                parameters.Add("NombreProducto", pedido.NombreProducto, DbType.String);
                parameters.Add("FechaPedido", pedido.FechaPedido, DbType.String);
                parameters.Add("fechaEntrega", pedido.fechaEntrega, DbType.String);
                parameters.Add("Estado", pedido.Estado, DbType.String);
                parameters.Add("EmailPedido", pedido.Descripcionl, DbType.String);

                const string query = @"INSERT INTO Pedido (IdPedido, NombrePedido, ApellidoPedido, EmailPedido) VALUES (@IdPedido, @NombrePedido, @ApellidoPedido, @EmailPedido)";
                await conn.ExecuteAsync(query, new { pedido.IdPeido, pedido.NombreProducto, pedido.FechaPedido, pedido.fechaEntrega }, commandType: CommandType.Text);
            }

            return true;
        }

        public async Task<Pedido> PedidoSelect(int id)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var query = @"SELECT IdPedido, NombrePedido, ApellidoPedido
                            FROM Pedido
                            WHERE IdPedido = @IdPedido";
                return await conn.QueryFirstOrDefaultAsync<Pedido>(query.ToString(), new { IdPedido = id }, commandType: CommandType.Text);
            }
        }

        public async Task<IEnumerable<Pedido>> GetAllPedido()
        {
            IEnumerable<Pedido> pedido;

            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string query = "SELECT IdPedido, NombrePedido, ApellidoPedido FROM Pedido";
                pedido = await conn.QueryAsync<Pedido>(query, commandType: CommandType.Text);
            }

            return pedido;
        }
    }
}