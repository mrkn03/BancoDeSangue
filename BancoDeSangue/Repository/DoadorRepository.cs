using BancoDeSangue.Data;
using BancoDeSangue.Models;
using BancoDeSangue.Repository.Interfaces;

namespace BancoDeSangue.Repository
{
    public class DoadorRepository : IDoadorRepository
    {
        private readonly BancoDeSangueContext context;

        public DoadorRepository(BancoDeSangueContext context)
        {
            this.context = context;
        }


        public Doador CadastrarDoador(Doador doador)
        {
            if(doador is null)
            {
                throw new ArgumentNullException("Doador nulo");
            }

            context.Add(doador);
            context.SaveChanges();

            return doador;
        }
        public Doador AtualizarDoador(Doador doador)
        {
            if(doador is null)
            {
                throw new ArgumentNullException("Doador nao pode ser nulo");
            }

            context.Entry(doador).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

            return doador;
        }

        public Doador DeletarDoador(string cpf)
        {
            var doador = context.Doadores.FirstOrDefault(doador => doador.Cpf == cpf);

            if(doador is null)
            {
                throw new ArgumentNullException("Doador nao encontrado");
            }

            context.Doadores.Remove(doador);
            context.SaveChanges();

            return doador;
        }

        public IEnumerable<Doador> ListarDoadores()
        {
            var doadores = context.Doadores.ToList();

            return doadores;
        }

        public Doador RecuperarDoador(string cpf)
        {
            var doador = context.Doadores.FirstOrDefault(doador => doador.Cpf == cpf);

            if(doador is null)
            {
                throw new ArgumentNullException("Doador nao encontrado");
            }

            return doador;
        }
    }
}
