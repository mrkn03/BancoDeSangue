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

    }
}
