using Desafio.SisGerTarefas.Domain.Entity;
using Desafio.SisGerTarefas.Domain.Repository;
using Desafio.SisGerTarefas.Infra.Data.EF.Context;
using Microsoft.EntityFrameworkCore;

namespace Desafio.SisGerTarefas.Infra.Data.EF.Repositories
{
    public class TarefaRepository
        : ITarefaRepository
    {
        private readonly TarefaDbContext _context;

        private DbSet<Tarefa> _tarefas
            => _context.Set<Tarefa>();

        public TarefaRepository(TarefaDbContext context)
            => _context = context;

        public async Task Insert(Tarefa aggregate, CancellationToken cancellationToken)
            => await _tarefas.AddAsync(aggregate, cancellationToken);

        public Task Delete(Tarefa aggregate, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Tarefa> Get(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        

        public Task Update(Tarefa aggregate, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
