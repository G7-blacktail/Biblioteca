using biblioteca_api.Models;
using Microsoft.EntityFrameworkCore;

namespace biblioteca_api.Data
{
    public class BibliotecaContext : DbContext
    {
        public BibliotecaContext(DbContextOptions<BibliotecaContext> options) : base(options) { }

        // DB Sets que representam as tabelas do seu banco de dados
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Locacao> Locacoes { get; set; }

    }
}
