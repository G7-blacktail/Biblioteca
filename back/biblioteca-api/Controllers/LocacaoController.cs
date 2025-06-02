using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; 
using System; 

using biblioteca_api.Models;
using biblioteca_api.Repositories;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class LocacoesController : ControllerBase
    {
        private readonly ILocacaoRepository _locacaoRepository;

        public LocacoesController(ILocacaoRepository locacaoRepository)
        {
            _locacaoRepository = locacaoRepository;
        }

        // GET: api/Locacoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Locacao>>> GetLocacoes()
        {
            var locacoes = await _locacaoRepository.GetAllLocacoesAsync();
            return Ok(locacoes); // Retorna 200 OK com a lista de locações
        }

        // GET: api/Locacoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Locacao>> GetLocacao(int id)
        {
            var locacao = await _locacaoRepository.GetLocacaoByIdAsync(id);
            if (locacao == null)
            {
                return NotFound(); // Retorna 404 Not Found
            }
            return Ok(locacao); // Retorna 200 OK com a locação
        }

        // POST: api/Locacoes - Registrar nova locação
        [HttpPost]
        public async Task<ActionResult<Locacao>> PostLocacao(Locacao locacao)
        {
            // Preencher campos que o usuário não precisa enviar
            locacao.DtRetirada = DateTime.UtcNow; // Data de retirada agora
            locacao.StLocacao = "Pendente"; // Status inicial
            locacao.VlMulta = 0.00m; // Multa inicial

            // A lógica de validação de disponibilidade e decremento está em LocarLivroAsync
            bool sucessoLocacao = await _locacaoRepository.LocarLivroAsync(locacao);

            if (!sucessoLocacao)
            {
                return BadRequest("Não foi possível realizar a locação. Verifique a disponibilidade do livro ou se os IDs de livro/usuário são válidos.");
            }

            // Retorna 201 Created e a URL do novo recurso (com o ID gerado pelo DB)
            return CreatedAtAction(nameof(GetLocacao), new { id = locacao.IdLocacao }, locacao);
        }

        // POST: api/Locacoes/{id}/devolver - Registrar devolução de um livro
        [HttpPost("{id}/devolver")]
        public async Task<IActionResult> DevolverLivro(int id)
        {
            var locacaoAtualizada = await _locacaoRepository.DevolverLivroAsync(id);

            if (locacaoAtualizada == null)
            {
                return NotFound("Locação não encontrada ou já devolvida.");
            }

            // Retorna 200 OK com a locação atualizada (incluindo a multa, se houver)
            return Ok(locacaoAtualizada);
        }


        // PUT: api/Locacoes/5 - Atualizar locação (para renovação ou outras edições)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocacao(int id, Locacao locacao)
        {
            if (id != locacao.IdLocacao)
            {
                return BadRequest("ID da locação não corresponde ao ID da rota.");
            }

            var existingLocacao = await _locacaoRepository.GetLocacaoByIdAsync(id);
            if (existingLocacao == null)
            {
                return NotFound();
            }

            // Não permitir alterar idLivro ou idUsuario diretamente no PUT sem lógica específica
            locacao.IdLivro = existingLocacao.IdLivro;
            locacao.IdUsuario = existingLocacao.IdUsuario;
            locacao.DtRetirada = existingLocacao.DtRetirada; // Não altera data de retirada no PUT

            try
            {
                await _locacaoRepository.UpdateLocacaoAsync(locacao);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _locacaoRepository.LocacaoExistsAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); // 204 No Content para atualização bem-sucedida
        }

        // DELETE: api/Locacoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocacao(int id)
        {
            var locacao = await _locacaoRepository.GetLocacaoByIdAsync(id);
            if (locacao == null)
            {
                return NotFound(); // Locação não encontrada
            }

            await _locacaoRepository.DeleteLocacaoAsync(id);
            return NoContent(); // 204 No Content
        }
    }
}