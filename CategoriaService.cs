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
    public class CategoriaService : ICategoriaService
    {
        private readonly SqlConnectionConfiguration _configuration;

        public CategoriaService()
        {
        }

        public CategoriaService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> CategoriaInsert(Categoria categoria)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("IdCategoria", categoria.IdCategoria, DbType.Int32);
                parameters.Add("Marca", categoria.Marca, DbType.String);
                parameters.Add("Descripcion", categoria.Descripcion, DbType.String);
                parameters.Add("Oferta", categoria.Oferta, DbType.String);

                const string query = @"INSERT INTO Categoria (IdCategoria, Marca, Descripcion, Oferta,) VALUES (@IdCategoria, @Marca, @Descripcion, @Oferta,)";
                await conn.ExecuteAsync(query, new { categoria.IdCategoria, categoria.Marca, categoria.Descripcion, categoria.Oferta }, commandType: CommandType.Text);
            }

            return true;
        }

        public async Task<Categoria> CategoriaSelect(int id)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var query = @"SELECT IdCategoria, Marca, Descripcion
                            FROM Categoria
                            WHERE IdCategoria = @IdCategoria";
                return await conn.QueryFirstOrDefaultAsync<Categoria>(query.ToString(), new { IdCategoria = id }, commandType: CommandType.Text);
            }
        }

        public async Task<IEnumerable<Categoria>> GetAllCategoria()
        {
            IEnumerable<Categoria> categoria;

            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string query = "SELECT Categoria, Marca, Descricion FROM Categoria";
                categoria = await conn.QueryAsync<Categoria>(query, commandType: CommandType.Text);
            }

            return categoria;
        }
    }

}
