using System.ComponentModel.DataAnnotations;

namespace BancoDeSangue.Models
{
    public class EstoqueDeSangue
    {
        [Key]
        public int Id { get; set; }

        public int TotalEstoque { get; set; } = 0;
        public int TotalOPositivo { get; set; } = 0;
        public int TotalONegativo { get; set; } = 0;
        public int TotalAPositivo { get; set; } = 0;
        public int TotalANegativo { get; set; } = 0;
        public int TotalBPositivo { get; set; } = 0;
        public int TotalBNegativo { get; set; } = 0;
        public int TotalABPositivo { get; set; } = 0;
        public int TotalABNegativo { get; set; } = 0;
    }
}
