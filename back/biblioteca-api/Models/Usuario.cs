using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace biblioteca_api.Models
{
    [Table("tb_usuario")]
    public class Usuario
    {
        [Key]
        [Column("id_usuario")]
        public int IdUsuario { get; set; }

        [Column("nm_usuario")]
        [Required]
        [StringLength(255)]
        public string NmUsuario { get; set; }

        [Column("nm_email")]
        [Required]
        [StringLength(255)]
        public string NmEmail { get; set; }

        [Column("nr_telefone")]
        [StringLength(20)]
        public string NrTelefone { get; set; }

        [Column("ds_senha")]
        [Required]
        [StringLength(255)]
        public string DsSenha { get; set; }

        [Column("tp_perfil")]
        [Required]
        [StringLength(50)]
        public string TpPerfil { get; set; }

        public ICollection<Locacao> Locacoes { get; set; } = new List<Locacao>();
    }
}