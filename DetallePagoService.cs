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
    public class DetallePagoService : IDetallePagoService
    {
        private readonly SqlConnectionConfiguration _configuration;

        public DetallePagoService()
        {
        }

        public DetallePagoService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> DetallePagoInsert(DetallePago detallePago)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("IdPago", detallePago.IdPago, DbType.Int32);
                parameters.Add("NombreProducto", detallePago.NombreProducto, DbType.String);
                parameters.Add("CantidadProducto", detallePago.CantidadProducto, DbType.String);

                parameters.Add("PrecioProducto", detallePago.PrecioProducto, DbType.Int32);
                parameters.Add("Descuento", detallePago.Descuento, DbType.Int32);
                parameters.Add("ValorTotal", detallePago.ValorTotal, DbType.Int32);


                const string query = @"INSERT INTO DetallePago (IdPago, NombreProducto, CantidadProducto, PrecioProducto, Descuento, ValorTotal ) VALUES (@IdPago, @NombreProducto, @CantidadProducto, @PrecioProducto, @Descuento, @ValorTotal )";
                await conn.ExecuteAsync(query, new { detallePago.IdPago, detallePago.NombreProducto, detallePago.CantidadProducto }, commandType: CommandType.Text);
            }

            return true;
        }

        public async Task<DetallePago> DetallePagoSelect(int id)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var query = @"SELECT IdPago, NombreProducto, ApellidoDetallePago
                            FROM DetallePago
                            WHERE IdPago = @IdPago";
                return await conn.QueryFirstOrDefaultAsync<DetallePago>(query.ToString(), new { IdPago = id }, commandType: CommandType.Text);
            }
        }

        public async Task<IEnumerable<DetallePago>> GetAllDetallePagos()
        {
            IEnumerable<DetallePago> detallePagos;

            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string query = "SELECT IdPago, NombreProducto, ApellidoDetallePago FROM DetallePago";
                detallePagos = await conn.QueryAsync<DetallePago>(query, commandType: CommandType.Text);
            }

            return detallePagos;
        }
    }
}