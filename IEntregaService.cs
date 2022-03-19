using BlazorApp1.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp1.Data.Service
{
    public interface IEntregaService
    {
        Task<bool> EntregaInsert(Entrega entrega);
        Task<Entrega> EntregaSelect(int id);
        Task<IEnumerable<Entrega>> GetAllEntregas();
    }
}