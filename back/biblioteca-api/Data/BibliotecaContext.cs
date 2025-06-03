using biblioteca_api.Models;
using Microsoft.EntityFrameworkCore;

namespace biblioteca_api.Data
{
    public class BibliotecaContext : DbContext
    {
        public BibliotecaContext(DbContextOptions<BibliotecaContext> options) : base(options) { }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Locacao> Locacoes { get; set; }

    }
}
