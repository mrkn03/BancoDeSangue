using System.ComponentModel.DataAnnotations;

namespace BancoDeSangue.Models
{
    public class Doacao
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid DoadorId { get; set; }

        public DateTime Data { get; set; }
        public int QuantidadeML { get; set; }

        public Doador Doador { get; set; }
    }
}
