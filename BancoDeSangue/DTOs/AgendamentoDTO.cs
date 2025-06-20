using BancoDeSangue.Models;

namespace BancoDeSangue.DTOs
{
    public class AgendamentoDTO
    {
        public int AgendamentoId { get; set; }
        public int DoadorId { get; set; }
        public string NomeDoador { get; set; }
        public string LocalColeta { get; set; }
        public string? Observacoes { get; set; }
        public DateTime Data { get; set; }
    }
}
