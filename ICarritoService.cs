using BlazorApp1.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp1.Data.Service
{
    public interface ICarritoService
    {
        Task<bool> CarritoInsert(Carrito carrito);
        Task<Carrito> CarritoSelect(int id);
        Task<IEnumerable<Carrito>> GetAllCarrito();
    }
}