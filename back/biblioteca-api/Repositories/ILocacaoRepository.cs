using biblioteca_api.Models;

namespace biblioteca_api.Repositories
{
    public interface ILocacaoRepository
    {
        Task<IEnumerable<Locacao>> GetAllLocacoesAsync();
        Task<Locacao> GetLocacaoByIdAsync(int id);
        Task AddLocacaoAsync(Locacao locacao);
        Task UpdateLocacaoAsync(Locacao locacao);
        Task DeleteLocacaoAsync(int id);
        Task<bool> LocacaoExistsAsync(int id);

        // Métodos específicos para a lógica de negócio
        Task<bool> LocarLivroAsync(Locacao locacao);
        Task<Locacao> DevolverLivroAsync(int idLocacao);
    }
}
