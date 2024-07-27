using Desafio.SisGerTarefas.Application.Interfaces;
using Desafio.SisGerTarefas.Infra.Data.EF.Context;

namespace Desafio.SisGerTarefas.Infra.Data.EF
{
    public class UnitOfWork
        : IUnitOfWork
    {
        private readonly TarefaDbContext _context;

        public UnitOfWork(TarefaDbContext context)
         => _context = context;

        public Task Commit(CancellationToken cancellationToken)
         => _context.SaveChangesAsync(cancellationToken);

        public Task Rollback(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}
