using BlazorApp1.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp1.Data.Service
{
    public interface ICategoriaService
    {
        Task<bool> CategoriaInsert(Categoria categoria);
        Task<Categoria> CategoriaSelect(int id);
        Task<IEnumerable<Categoria>> GetAllCategoria();
    }
}