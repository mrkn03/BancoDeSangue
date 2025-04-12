using BancoDeSangue.Models;
using Microsoft.EntityFrameworkCore;

namespace BancoDeSangue.Data
{
    public class BancoDeSangueContext : DbContext
    {
        public BancoDeSangueContext(DbContextOptions<BancoDeSangueContext> options)
        : base(options) { }

        public DbSet<Doador> Doadores { get; set; }
        public DbSet<Doacao> Doacoes { get; set; }
        public DbSet<EstoqueDeSangue> Estoques { get; set; }
        public object EstoqueDeSangue { get; internal set; }
    }
}
