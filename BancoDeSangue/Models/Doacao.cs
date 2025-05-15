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

        [Required]
        [ForeignKey("DoadorId")]
        public Doador Doador { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade de sangue doada deve ser maior que zero.")]
        public int QuantidadeML { get; set; }

        [Required]
        [Column(TypeName = "date")]
        [CustomValidation(typeof(Doacao), nameof(ValidarData))]
        public DateTime Data { get; set; }

        public Doacao()
        {
            Data = DateTime.Now;
        }

        // Método de validação  
        public static ValidationResult? ValidarData(DateTime data, ValidationContext context)
        {
            if (data > DateTime.Now)
            {
                return new ValidationResult("A data da doação não pode ser no futuro.");
            }
            return ValidationResult.Success;
        }
    }
}
