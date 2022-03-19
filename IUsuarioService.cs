using BlazorApp1.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp1.Data.Service
{
    public interface IUsuarioService
    {
        Task<IEnumerable<Usuario>> GetAllUsuario();
        Task<bool> UsuarioInsert(Usuario usuario);
        Task<Usuario> UsuarioSelect(int id);
    }
}