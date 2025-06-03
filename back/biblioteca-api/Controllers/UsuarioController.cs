using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using biblioteca_api.Models;
using biblioteca_api.Repositories;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        private ILocacaoRepository _locacaoRepository;

        public UsuariosController(IUsuarioRepository usuarioRepository, ILocacaoRepository locacaoRepository)
        {
            _locacaoRepository = locacaoRepository;
            _usuarioRepository = usuarioRepository;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            var usuarios = await _usuarioRepository.GetAllUsuariosAsync();
            // Importante: Limpar a senha antes de retornar
            foreach (var usuario in usuarios)
            {
                usuario.DsSenha = null;
            }
            return Ok(usuarios);
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _usuarioRepository.GetUsuarioByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            usuario.DsSenha = null;
            return Ok(usuario);
        }

        // POST: api/Usuarios
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            // O Hashing da senha agora ocorre no repositório AddUsuarioAsync
            await _usuarioRepository.AddUsuarioAsync(usuario);

            usuario.DsSenha = null;
            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.IdUsuario }, usuario);
        }

        // PUT: api/Usuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.IdUsuario)
            {
                return BadRequest("ID do usuário não corresponde ao ID da rota.");
            }

            if (!await _usuarioRepository.UsuarioExistsAsync(id))
            {
                return NotFound();
            }

            try
            {
                await _usuarioRepository.UpdateUsuarioAsync(usuario);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _usuarioRepository.UsuarioExistsAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _usuarioRepository.GetUsuarioByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            await _usuarioRepository.DeleteUsuarioAsync(id);
            return NoContent();
        }
        
        [HttpGet("usuario/{idUsuario}")]
        public async Task<ActionResult<IEnumerable<LocacaoDashboardRepresentation>>> GetLocacoesUsuario(int idUsuario)
        {
            var locacoes = await _locacaoRepository.GetLocacoesDashboardUsuarioAsync(idUsuario);
            return Ok(locacoes);
        }
    }
}