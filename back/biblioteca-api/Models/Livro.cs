using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace biblioteca_api.Models
{
    [Table("tb_livro")]
    public class Livro
    {
        [Key] 
        [Column("id_livro")] 
        public int IdLivro { get; set; }

        [Column("nm_titulo")]
        [Required] 
        [StringLength(255)]
        public string NmTitulo { get; set; }

        [Column("nm_autor")]
        [Required]
        [StringLength(255)]
        public string NmAutor { get; set; }

        [Column("nm_editora")]
        [StringLength(255)]
        public string NmEditora { get; set; }

        [Column("aa_publicacao")]
        public int? AaPublicacao { get; set; }

        [Column("isbn_livro")]
        [Required]
        [StringLength(20)]
        public string IsbnLivro { get; set; }

        [Column("qt_disponivel")]
        public int QtDisponivel { get; set; } = 0; 

        public ICollection<Locacao> Locacoes { get; set; } = new List<Locacao>();
    }
}