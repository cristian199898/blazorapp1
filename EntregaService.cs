using Dapper;
using Microsoft.Data.SqlClient;
 
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp1.Data;
using BlazorApp1.Data.Model;


namespace BlazorApp1.Data.Service
{
    public class EntregaService : IEntregaService
    {
        private readonly SqlConnectionConfiguration _configuration;

        public EntregaService()
        {
        }

        public EntregaService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> EntregaInsert(Entrega entrega)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("IdCliente", entrega.IdCliente, DbType.Int32);
                parameters.Add("NombreCliente", entrega.NombreCliente, DbType.String);
                parameters.Add("Direccion", entrega.Direccion, DbType.String);


                const string query = @"INSERT INTO Entrega (IdCliente, NombreCliente, Direccion ) VALUES (@IdCliente, @NombreCliente, @Direccion )";
                await conn.ExecuteAsync(query, new { entrega.IdCliente, entrega.NombreCliente, entrega.Direccion }, commandType: CommandType.Text);
            }

            return true;
        }

        public async Task<Entrega> EntregaSelect(int id)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var query = @"SELECT IdCliente, NombreCliente, ApellidoEntrega
                            FROM Entrega
                            WHERE IdCliente = @IdCliente";
                return await conn.QueryFirstOrDefaultAsync<Entrega>(query.ToString(), new { IdCliente = id }, commandType: CommandType.Text);
            }
        }

        public async Task<IEnumerable<Entrega>> GetAllEntregas()
        {
            IEnumerable<Entrega> entregas;

            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string query = "SELECT IdCliente, NombreCliente, ApellidoEntrega FROM Entrega";
                entregas = await conn.QueryAsync<Entrega>(query, commandType: CommandType.Text);
            }

            return entregas;
        }
    }
}