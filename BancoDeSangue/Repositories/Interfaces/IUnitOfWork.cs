using BancoDeSangue.Repository.Interfaces;

namespace BancoDeSangue.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IAgendamentoRepository AgendamentoRepository { get; }
        IDoacaoRepository DoacaoRepository { get; }
        IDoadorRepository DoadorRepository { get; }
        IEstoqueDeSangueRepository EstoqueDeSangueRepository { get; }

        Task CommitAsync();
        void Dispose();
    }
}
