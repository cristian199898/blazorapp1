using BlazorApp1.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp1.Data.Service
{
    public interface IProductoService
    {
        Task<IEnumerable<Producto>> GetAllProductos();
        Task<bool> ProductoInsert(Producto producto);
        Task<Producto> ProductoSelect(int id);
    }
}
