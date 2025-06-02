using biblioteca_api.Data;
using biblioteca_api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace biblioteca_api.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly BibliotecaContext _context;

        public UsuarioRepository(BibliotecaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> GetAllUsuariosAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario> GetUsuarioByIdAsync(int id)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == id);
        }

        public async Task AddUsuarioAsync(Usuario usuario)
        {
            // **HASHING DA SENHA ANTES DE SALVAR**
            if (!string.IsNullOrEmpty(usuario.DsSenha))
            {
                usuario.DsSenha = BCrypt.Net.BCrypt.HashPassword(usuario.DsSenha);
            }

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUsuarioAsync(Usuario usuario)
        {
            var existingUser = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.IdUsuario == usuario.IdUsuario);

            if (existingUser == null)
            {
                return; // Ou lançar uma exceção de que o usuário não existe
            }

            // **HASHING DA SENHA SE FOR ATUALIZADA**
            // Se a senha foi enviada no corpo da requisição PUT (e não está vazia),
            // assume-se que é uma nova senha e deve ser hasheada.
            // Caso contrário, mantém a senha existente no banco de dados.
            if (!string.IsNullOrEmpty(usuario.DsSenha) && usuario.DsSenha != existingUser.DsSenha)
            {
                usuario.DsSenha = BCrypt.Net.BCrypt.HashPassword(usuario.DsSenha);
            }
            else
            {
                // Se a senha não foi fornecida ou não foi alterada, mantenha o hash existente
                usuario.DsSenha = existingUser.DsSenha;
            }

            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUsuarioAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Usuario> GetUsuarioByEmailAsync(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.NmEmail == email);
        }

        public async Task<bool> UsuarioExistsAsync(int id)
        {
            return await _context.Usuarios.AnyAsync(e => e.IdUsuario == id);
        }
    }
}