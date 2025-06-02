using biblioteca_api.Models;
using biblioteca_api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace biblioteca_api.Repositories
{
        public class LivroRepository : ILivroRepository
        {
            private readonly BibliotecaContext _context;

            public LivroRepository(BibliotecaContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Livro>> GetAllLivrosAsync()
            {
                return await _context.Livros.ToListAsync();
            }

            public async Task<Livro> GetLivroByIdAsync(int id)
            {
                return await _context.Livros.FirstOrDefaultAsync(l => l.IdLivro == id);
            }

            public async Task AddLivroAsync(Livro livro)
            {
                _context.Livros.Add(livro);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateLivroAsync(Livro livro)
            {
                _context.Entry(livro).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            public async Task DeleteLivroAsync(int id)
            {
                var livro = await _context.Livros.FindAsync(id);
                if (livro != null)
                {
                    _context.Livros.Remove(livro);
                    await _context.SaveChangesAsync();
                }
            }

            public async Task<string> RegistrarLocacaoAsync(int idLivro, int idUsuario, DateTime dtRetirada, DateTime dtDevolucaoPrevista)
            {
                var parameters = new[]
                {
                new SqlParameter("@id_livro", idLivro),
                new SqlParameter("@id_usuario", idUsuario),
                new SqlParameter("@dt_retirada", dtRetirada),
                new SqlParameter("@dt_devolucao_prevista", dtDevolucaoPrevista)
            };

                await _context.Database.ExecuteSqlRawAsync("EXEC sp_registrar_locacao @id_livro, @id_usuario, @dt_retirada, @dt_devolucao_prevista", parameters);

                return "Processo de locação iniciado. Verifique o banco de dados para o status final.";
            }

            public async Task<string> RegistrarDevolucaoAsync(int idLocacao, DateTime dtDevolucaoReal)
            {
                var parameters = new[]
               {
                new SqlParameter("@id_locacao", idLocacao),
                new SqlParameter("@dt_devolucao_real", dtDevolucaoReal)
            };

                await _context.Database.ExecuteSqlRawAsync("EXEC sp_registrar_devolucao @id_locacao, @dt_devolucao_real", parameters);

                return "Processo de devolução iniciado. Verifique o banco de dados para o status final e multa.";
            }
        }
    
}
