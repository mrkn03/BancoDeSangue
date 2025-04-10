using System.ComponentModel.DataAnnotations;

namespace BancoDeSangue.Models
{
    public class Doador
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Nome { get; set; }

        [Required]
        [StringLength(11)]
        public string Cpf { get; set; }

        public int Idade { get; set; }
        public string Telefone { get; set; }
        public string TipoSanguineo { get; set; }
        public DateTime? UltimaDoacao { get; set; }

        public ICollection<Doacao> Doacoes { get; set; }
    }
}
