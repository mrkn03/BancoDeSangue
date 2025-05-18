    using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace BancoDeSangue.Models
{
    [Table("Doador")]
    public class Doador
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        [StringLength(11)]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deve conter exatamente 11 dígitos numéricos.")]
        public string Cpf { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime DataNascimento { get; set; }

        [NotMapped]
        public int Idade => DateTime.Now.Year - DataNascimento.Year -
            (DateTime.Now.DayOfYear < DataNascimento.DayOfYear ? 1 : 0);


        [StringLength(15, ErrorMessage = "O telefone deve ter no máximo 15 caracteres.")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O tipo sanguíneo é obrigatório.")]
        [RegularExpression(@"^(A|B|AB|O)[+-]$", ErrorMessage = "Tipo sanguíneo inválido.")]
        public string TipoSanguineo { get; set; }

        [JsonIgnore]
        [Column(TypeName = "date")]
        public DateTime? UltimaDoacao { get; set; }

        [JsonIgnore]
        public ICollection<Doacao> Doacoes { get; private set; } = new Collection<Doacao>();

        public Doador()
        {
            Doacoes = new Collection<Doacao>();
        }

    }
}