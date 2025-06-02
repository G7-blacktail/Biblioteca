using Microsoft.AspNetCore.Mvc;
using biblioteca_api.Repositories;
using biblioteca_api.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace biblioteca_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivrosController : ControllerBase
    {
        private readonly ILivroRepository _livroRepository;

        public LivrosController(ILivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }

        // GET: api/Livros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Livro>>> GetLivros()
        {
            var livros = await _livroRepository.GetAllLivrosAsync();
            return Ok(livros); // Retorna 200 OK com a lista de livros
        }

        // GET: api/Livros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Livro>> GetLivro(int id)
        {
            var livro = await _livroRepository.GetLivroByIdAsync(id);
            if (livro == null)
            {
                return NotFound(); // Retorna 404 Not Found
            }
            return Ok(livro); // Retorna 200 OK com o livro
        }

        // POST: api/Livros
        [HttpPost]
        public async Task<ActionResult<Livro>> PostLivro(Livro livro)
        {
            await _livroRepository.AddLivroAsync(livro);
            // Retorna 201 Created e a URL do novo recurso
            return CreatedAtAction(nameof(GetLivro), new { id = livro.IdLivro }, livro);
        }

        // PUT: api/Livros/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLivro(int id, Livro livro)
        {
            if (id != livro.IdLivro)
            {
                return BadRequest("ID do livro não corresponde ao ID da rota."); // Retorna 400 Bad Request
            }

            try
            {
                await _livroRepository.UpdateLivroAsync(livro);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _livroRepository.GetLivroByIdAsync(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); // Retorna 204 No Content (sucesso sem retorno de conteúdo)
        }

        // DELETE: api/Livros/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLivro(int id)
        {
            var livro = await _livroRepository.GetLivroByIdAsync(id);
            if (livro == null)
            {
                return NotFound();
            }

            await _livroRepository.DeleteLivroAsync(id);
            return NoContent();
        }

    }

}
