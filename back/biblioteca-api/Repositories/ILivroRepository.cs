using biblioteca_api.Models;

namespace biblioteca_api.Repositories
{
    public interface ILivroRepository
    {
        public interface ILivroRepository
        {
            Task<IEnumerable<Livro>> GetAllLivrosAsync();
            Task<Livro> GetLivroByIdAsync(int id);
            Task AddLivroAsync(Livro livro);
            Task UpdateLivroAsync(Livro livro);
            Task DeleteLivroAsync(int id);
            Task<string> RegistrarLocacaoAsync(int idLivro, int idUsuario, DateTime dtRetirada, DateTime dtDevolucaoPrevista);
            Task<string> RegistrarDevolucaoAsync(int idLocacao, DateTime dtDevolucaoReal);
        }
    }
}
