using biblioteca_api.Data;
using biblioteca_api.Models;
using biblioteca_api.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace biblioteca_api.Repositories
{
    public class LocacaoRepository : ILocacaoRepository
    {
        private readonly BibliotecaContext _context;
        private readonly ILivroRepository _livroRepository;

        public LocacaoRepository(BibliotecaContext context, ILivroRepository livroRepository)
        {
            _context = context;
            _livroRepository = livroRepository;
        }

        public async Task<IEnumerable<Locacao>> GetAllLocacoesAsync()
        {
            return await _context.Locacoes
                                 .Include(l => l.Livro)
                                 .Include(l => l.Usuario)
                                 .ToListAsync();
        }

        public async Task<Locacao> GetLocacaoByIdAsync(int id)
        {
            return await _context.Locacoes
                                 .Include(l => l.Livro)
                                 .Include(l => l.Usuario)
                                 .FirstOrDefaultAsync(l => l.IdLocacao == id);
        }

        public async Task AddLocacaoAsync(Locacao locacao)
        {
            _context.Locacoes.Add(locacao);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLocacaoAsync(Locacao locacao)
        {
            _context.Entry(locacao).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLocacaoAsync(int id)
        {
            var locacao = await _context.Locacoes.FindAsync(id);
            if (locacao != null)
            {
                _context.Locacoes.Remove(locacao);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> LocacaoExistsAsync(int id)
        {
            return await _context.Locacoes.AnyAsync(e => e.IdLocacao == id);
        }

        public async Task<bool> LocarLivroAsync(Locacao locacao)
        {
            var livro = await _livroRepository.GetLivroByIdAsync(locacao.IdLivro);

            if (livro == null || livro.QtDisponivel <= 0)
            {
                return false;
            }

            // Diminuir a quantidade disponível do livro
            livro.QtDisponivel--;
            await _livroRepository.UpdateLivroAsync(livro);

            // Definir status da locação
            locacao.StLocacao = "Pendente";
            locacao.VlMulta = 0; // Inicia a multa em 0

            // Adicionar a locação
            _context.Locacoes.Add(locacao);
            await _context.SaveChangesAsync();

            return true; // Locação bem-sucedida
        }

        // Lógica de negócio para devolver um livro
        public async Task<Locacao> DevolverLivroAsync(int idLocacao)
        {
            var locacao = await _context.Locacoes
                                        .Include(l => l.Livro)
                                        .FirstOrDefaultAsync(l => l.IdLocacao == idLocacao);

            if (locacao == null || locacao.StLocacao == "Devolvido")
            {
                return null; // Locação não encontrada ou já devolvida
            }

            // Aumentar a quantidade disponível do livro
            locacao.Livro.QtDisponivel++;
            await _livroRepository.UpdateLivroAsync(locacao.Livro); // Atualiza o livro

            // Calcular multa (exemplo simples: R$ 1 por dia de atraso)
            locacao.DtDevolucaoReal = DateTime.UtcNow; // Data de devolução real
            if (locacao.DtDevolucaoReal > locacao.DtDevolucaoPrevista)
            {
                TimeSpan atraso = locacao.DtDevolucaoReal.Value - locacao.DtDevolucaoPrevista;
                locacao.VlMulta = (decimal)Math.Ceiling(atraso.TotalDays) * 1.00m; // R$1 por dia
            }
            else
            {
                locacao.VlMulta = 0;
            }

            locacao.StLocacao = "Devolvido"; // Marcar como devolvido

            _context.Entry(locacao).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return locacao; // Retorna a locação atualizada
        }

        public async Task<IEnumerable<LocacaoDashboardRepresentation>> GetAllLocacoesDashboardAsync()
        {
            return await _context.Locacoes
                .Include(l => l.Livro)
                .Include(l => l.Usuario)
                .Select(l => new LocacaoDashboardRepresentation
                {
                    IdLocacao = l.IdLocacao,
                    LivroTitulo = l.Livro.NmTitulo,
                    UsuarioNome = l.Usuario.NmUsuario,
                    UsuarioEmail = l.Usuario.NmEmail,
                    StLocacao = l.StLocacao,
                    PagamentoPendente = l.StLocacao == "Pendente",
                    VlMulta = l.VlMulta,
                    DtRetirada = l.DtRetirada,
                    DtDevolucaoPrevista = l.DtDevolucaoPrevista,
                    DtDevolucaoReal = l.DtDevolucaoReal
                })
                .ToListAsync();
        }
        
        public async Task<IEnumerable<LocacaoDashboardRepresentation>> GetLocacoesDashboardUsuarioAsync(int idUsuario)
        {
            return await _context.Locacoes
                .Include(l => l.Livro)
                .Where(l => l.IdUsuario == idUsuario)
                .Select(l => new LocacaoDashboardRepresentation
                {
                    IdLocacao = l.IdLocacao,
                    LivroTitulo = l.Livro.NmTitulo,
                    StLocacao = l.StLocacao,
                    DtDevolucaoPrevista = l.DtDevolucaoPrevista,
                    DtDevolucaoReal = l.DtDevolucaoReal,
                    VlMulta = l.VlMulta,
                    PagamentoPendente = l.StLocacao == "Pendente"
                })
                .ToListAsync();
        }
    }
}