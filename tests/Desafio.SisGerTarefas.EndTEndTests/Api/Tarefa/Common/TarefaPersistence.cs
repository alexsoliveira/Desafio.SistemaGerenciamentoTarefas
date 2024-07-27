using Desafio.SisGerTarefas.Infra.Data.EF.Context;
using Microsoft.EntityFrameworkCore;
using DomainEntity = Desafio.SisGerTarefas.Domain.Entity;

namespace Desafio.SisGerTarefas.EndTEndTests.Api.Tarefa.Common
{
    public class TarefaPersistence
    {
        private readonly TarefaDbContext _context;

        public TarefaPersistence(TarefaDbContext context)
            => _context = context;

        public async Task<DomainEntity.Tarefa?> GetById(Guid id)
            => await _context.Tarefas.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
    }
}
