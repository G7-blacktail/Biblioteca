
using biblioteca_api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace biblioteca_api.Repositories
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetAllUsuariosAsync();
        Task<Usuario> GetUsuarioByIdAsync(int id);
        Task AddUsuarioAsync(Usuario usuario);
        Task UpdateUsuarioAsync(Usuario usuario);
        Task DeleteUsuarioAsync(int id);
        Task<Usuario> GetUsuarioByEmailAsync(string email);
        Task<bool> UsuarioExistsAsync(int id);
    }
}