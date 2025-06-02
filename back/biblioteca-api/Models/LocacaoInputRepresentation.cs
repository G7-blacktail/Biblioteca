using System;
using System.ComponentModel.DataAnnotations;

namespace biblioteca_api.Models
{
    public class LocacaoInputRepresentation
    {
        [Required(ErrorMessage = "O ID do livro é obrigatório.")]
        public int IdLivro { get; set; }

        [Required(ErrorMessage = "O ID do usuário é obrigatório.")]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "A data de devolução prevista é obrigatória.")]
        public DateTime DtDevolucaoPrevista { get; set; }
    }
}
