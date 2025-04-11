using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BancoDeSangue.Models
{
    [Table("Doacao")]
    public class Doacao
    {
        [Key]
        public int DoacaoId { get; set; }

        [Required]
        public int DoadorId { get; set; } 

        [ForeignKey("DoadorId")]
        public Doador Doador { get; set; }

        public DateTime Data { get; set; }

        [Required]
        public int QuantidadeML { get; set; }

        public Doacao()
        {
            Data = DateTime.Now;
        }
    }
}
