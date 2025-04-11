    using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BancoDeSangue.Models
{
    [Table("Doador")]
    public class Doador
    {
        [Key]
        public int DoadorId { get; set; } // PK para o banco

        [Required]
        public string Nome { get; set; }

        [Required]
        [StringLength(11)]
        public string Cpf { get; set; } // usado nas buscas (identificador lógico)

        public int Idade { get; set; }
        public string Telefone { get; set; }
        public string TipoSanguineo { get; set; }
        public DateTime? UltimaDoacao { get; set; }

        public ICollection<Doacao> Doacoes { get; set; }

        public Doador()
        {
            Doacoes = new Collection<Doacao>();
        }
    }
}