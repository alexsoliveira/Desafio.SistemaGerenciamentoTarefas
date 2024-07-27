using Desafio.SisGerTarefas.Domain.Exceptions;
using Desafio.SisGerTarefas.Infra.Data.EF;
using Desafio.SisGerTarefas.Infra.Data.EF.Repositories;
using ApplicationUseCase = Desafio.SisGerTarefas.Application.UseCases.Tarefa.CreateTarefa;
using FluentAssertions;
using Desafio.SisGerTarefas.Application.UseCases.Tarefa.CreateTarefa;
using Desafio.SisGerTarefas.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Desafio.SisGerTarefas.IntegrationTests.Application.UseCases.CreateTarefa
{
    [Collection(nameof(CreateTarefaTestFixture))]
    public class CreateTarefaTest
    {
        private readonly CreateTarefaTestFixture _fixture;

        public CreateTarefaTest(CreateTarefaTestFixture fixture)
            => _fixture = fixture;

        [Fact(DisplayName = nameof(CreateTarefa))]
        [Trait("Integration/Application", "CreateTarefa - Use Cases")]
        public async Task CreateTarefa()
        {
            var dbContext = _fixture.CreateDbContext();
            var repository = new TarefaRepository(dbContext);
            var unitOfWork = new UnitOfWork(dbContext);
            var useCase = new ApplicationUseCase.CreateTarefa(
                repository,
                unitOfWork
            );
            var input = _fixture.GetInput();
            var output = await useCase.Handle(input, CancellationToken.None);

            var dbTarefa = await (_fixture.CreateDbContext(true))
                .Tarefas.FindAsync(output.Id);
            dbTarefa.Should().NotBeNull();
            dbTarefa!.Titulo.Should().Be(input.Titulo);
            dbTarefa.Descricao.Should().Be(input.Descricao);
            dbTarefa.Status.Should().Be(input.Status);
            dbTarefa.DataVencimento.Should().Be(output.DataVencimento);
            output.Should().NotBeNull();
            output.Titulo.Should().Be(input.Titulo);
            output.Descricao.Should().Be(input.Descricao);
            output.Status.Should().Be(input.Status);
            output.Id.Should().NotBeEmpty();
            output.DataVencimento.Should().NotBeSameDateAs(default);
        }

        [Fact(DisplayName = nameof(CreateTarefaWithOnlyName))]
        [Trait("Integration/Application", "CreateTarefa - Use Cases")]
        public async void CreateTarefaWithOnlyName()
        {
            var dbContext = _fixture.CreateDbContext();
            var repository = new TarefaRepository(dbContext);
            var unitOfWork = new UnitOfWork(dbContext);
            var useCase = new ApplicationUseCase.CreateTarefa(
                repository,
                unitOfWork
            );
            var input = new CreateTarefaInput(
                _fixture.GetInput().Titulo
            );
            var output = await useCase.Handle(input, CancellationToken.None);

            var dbTarefa = await (_fixture.CreateDbContext(true))
                 .Tarefas.FindAsync(output.Id);
            dbTarefa.Should().NotBeNull();
            dbTarefa!.Titulo.Should().Be(input.Titulo);
            dbTarefa.Descricao.Should().Be("");
            dbTarefa.Status.Should().Be(Status.Pendente);
            dbTarefa.DataVencimento.Should().Be(output.DataVencimento);
            output.Should().NotBeNull();
            output.Titulo.Should().Be(input.Titulo);
            output.Descricao.Should().Be("");
            output.Status.Should().Be(Status.Pendente);
            output.Id.Should().NotBeEmpty();
            output.DataVencimento.Should().NotBeSameDateAs(default);
        }

        [Fact(DisplayName = nameof(CreateTarefaWithOnlyNameAndDescription))]
        [Trait("Integration/Application", "CreateTarefa - Use Cases")]
        public async void CreateTarefaWithOnlyNameAndDescription()
        {
            var dbContext = _fixture.CreateDbContext();
            var repository = new TarefaRepository(dbContext);
            var unitOfWork = new UnitOfWork(dbContext);
            var useCase = new ApplicationUseCase.CreateTarefa(
                repository,
                unitOfWork
            );
            var exampleInput = _fixture.GetInput();
            var input = new CreateTarefaInput(
                exampleInput.Titulo,
                exampleInput.Descricao
            );
            var output = await useCase.Handle(input, CancellationToken.None);

            var dbTarefa = await (_fixture.CreateDbContext(true))
                 .Tarefas.FindAsync(output.Id);
            dbTarefa.Should().NotBeNull();
            dbTarefa!.Titulo.Should().Be(input.Titulo);
            dbTarefa.Descricao.Should().Be(input.Descricao);
            dbTarefa.Status.Should().Be(Status.Pendente);
            dbTarefa.DataVencimento.Should().Be(output.DataVencimento);
            output.Should().NotBeNull();
            output.Titulo.Should().Be(input.Titulo);
            output.Descricao.Should().Be(input.Descricao);
            output.Status.Should().Be(Status.Pendente);
            output.Id.Should().NotBeEmpty();
            output.DataVencimento.Should().NotBeSameDateAs(default);
        }

        [Theory(DisplayName = nameof(ThrowWhenCantInstantiateTarefa))]
        [Trait("Integration/Application", "CreateTarefa - Use Cases")]
        [MemberData(
            nameof(CreateTarefaTestDataGenerator.GetInvalidInputs),
            parameters: 4,
            MemberType = typeof(CreateTarefaTestDataGenerator)
        )]
        public async void ThrowWhenCantInstantiateTarefa(
            CreateTarefaInput input,
            string exceptionMessage
        )
        {
            var dbContext = _fixture.CreateDbContext();
            var repository = new TarefaRepository(dbContext);
            var unitOfWork = new UnitOfWork(dbContext);
            var useCase = new ApplicationUseCase.CreateTarefa(
                repository,
                unitOfWork
            );

            Func<Task> task =
                async () => await useCase.Handle(input, CancellationToken.None);

            await task.Should()
                .ThrowAsync<EntityValidationException>()
                .WithMessage(exceptionMessage);

            var dbCategoriesList = _fixture.CreateDbContext(true)
                .Tarefas.AsNoTracking()
                .ToList();
            dbCategoriesList.Should().HaveCount(0);

        }
    }
}
