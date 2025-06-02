using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace biblioteca_api.Models
{
    [Table("tb_locacao")]
    public class Locacao
    {
        [Key]
        [Column("id_locacao")]
        public int IdLocacao { get; set; }

        [Column("id_livro")]
        public int IdLivro { get; set; }

        [Column("id_usuario")]
        public int IdUsuario { get; set; }

        [Column("dt_retirada")]
        public DateTime DtRetirada { get; set; }

        [Column("dt_devolucao_prevista")]
        public DateTime DtDevolucaoPrevista { get; set; }

        [Column("dt_devolucao_real")]
        public DateTime? DtDevolucaoReal { get; set; }

        [Column("vl_multa", TypeName = "decimal(10, 2)")]
        public decimal VlMulta { get; set; } = 0;

        [Column("st_locacao")]
        [Required]
        [StringLength(50)]
        public string StLocacao { get; set; }

        // Propriedades de navegação (para acesso direto aos objetos Livro e Usuario relacionados)
        [ForeignKey("IdLivro")]
        public Livro Livro { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }
    }
}