namespace biblioteca_api.Models
{
    public class LocacaoDashboardRepresentation
    {
        public int IdLocacao { get; set; }
        public string LivroTitulo { get; set; }
        public string UsuarioNome { get; set; }
        public string UsuarioEmail { get; set; }
        public string StLocacao { get; set; }
        public bool PagamentoPendente { get; set; }
        public decimal VlMulta { get; set; }
        public DateTime DtRetirada { get; set; }
        public DateTime DtDevolucaoPrevista { get; set; }
        public DateTime? DtDevolucaoReal { get; set; }
    }
}