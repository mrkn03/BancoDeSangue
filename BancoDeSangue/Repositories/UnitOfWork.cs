using BancoDeSangue.Data;
using BancoDeSangue.Repositories.Interfaces;
using BancoDeSangue.Repository;
using BancoDeSangue.Repository.Interfaces;

namespace BancoDeSangue.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDoadorRepository? doadorRepository;
        private IDoacaoRepository? doacaoRepository;
        private IAgendamentoRepository? agendamentoRepository;
        private IEstoqueDeSangueRepository? estoqueDeSangueRepository;

        public BancoDeSangueContext context;

        public IAgendamentoRepository AgendamentoRepository
        {
            get
            {
                return agendamentoRepository ??= new AgendamentoRepository(context);


            }
        }

        public IDoacaoRepository DoacaoRepository
        {
            get
            {
                return doacaoRepository ??= new DoacaoRepository(context);

            }
        }

        public IDoadorRepository DoadorRepository
        {
            get
            {
                return doadorRepository ??= new DoadorRepository(context);

            }
        }

        public IEstoqueDeSangueRepository EstoqueDeSangueRepository
        {
            get
            {
                return estoqueDeSangueRepository ??= new EstoqueDeSangueRepository(context);

            }
        }

        public UnitOfWork(BancoDeSangueContext context)
        {
            this.context = context;
        }

        public async Task CommitAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
