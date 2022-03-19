using BlazorApp1.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp1.Data.Service
{
    public interface IDetallePagoService
    {
        Task<bool> DetallePagoInsert(DetallePago detallePago);
        Task<DetallePago> DetallePagoSelect(int id);
        Task<IEnumerable<DetallePago>> GetAllDetallePagos();
    }
}