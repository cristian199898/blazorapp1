using BlazorApp1.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp1.Data.Service
{
    public interface IPedidoService
    {
        Task<IEnumerable<Pedido>> GetAllPedido();
        Task<bool> PedidoInsert(Pedido pedido);
        Task<Pedido> PedidoSelect(int id);
    }
}