using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BancoDeSangue.Models
{
    [Table("Agendamento")]
    public class Agendamento
    {
        [Key]
        [JsonIgnore]
        public int AgendamentoId { get; set; }

        //[Required]
        public int DoadorId { get; set; }

        [ForeignKey("DoadorId")]
        public required Doador Doador { get; set; }

        [Required(ErrorMessage = "A data do agendamento são obrigatórias.")]
        [Column(TypeName = "date")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "O local da coleta é obrigatório.")]
        [StringLength(100, ErrorMessage = "O local deve ter no máximo 100 caracteres.")]
        public required string LocalColeta { get; set; }

        [StringLength(200)]
        public string? Observacoes { get; set; }

        public static ValidationResult ValidarData(DateTime data, ValidationContext context)
        {
            if (data < DateTime.Now)
            {
                return new ValidationResult("A data do agendamento não pode estar no passado.");
            }
            return new ValidationResult(string.Empty);
        }
    }
}
