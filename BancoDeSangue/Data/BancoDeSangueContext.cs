using BancoDeSangue.Models;
using Microsoft.EntityFrameworkCore;

namespace BancoDeSangue.Data
{
    public class BancoDeSangueContext : DbContext
    {
        public BancoDeSangueContext(DbContextOptions<BancoDeSangueContext> options)
            : base(options)
        {
            Estoques = Set<EstoqueDeSangue>();
        }

        public DbSet<Doador> Doadores { get; set; }
        public DbSet<Doacao> Doacoes { get; set; }
        public DbSet<EstoqueDeSangue> Estoques { get; set; }
        public DbSet<Agendamento> Agendamentos { get; set; }
    }
}
