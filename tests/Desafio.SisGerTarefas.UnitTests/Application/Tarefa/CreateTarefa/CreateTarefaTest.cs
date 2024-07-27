using DomainEntity = Desafio.SisGerTarefas.Domain.Entity;
using UseCase = Desafio.SisGerTarefas.Application.UseCases.Tarefa.CreateTarefa;
using Moq;
using FluentAssertions;
using Desafio.SisGerTarefas.Domain.Exceptions;
using Desafio.SisGerTarefas.Application.UseCases.Tarefa.CreateTarefa;

namespace Desafio.SisGerTarefas.UnitTests.Application.Tarefa.CreateTarefa
{
    [Collection(nameof(CreateTarefaTestFixture))]
    public class CreateTarefaTest
    {
        private readonly CreateTarefaTestFixture _fixture;

        public CreateTarefaTest(CreateTarefaTestFixture fixture)
            => _fixture = fixture;

        [Fact(DisplayName = nameof(CreateTarefa))]
        [Trait("Application", "CreateTarefa - Use Cases")]
        public async void CreateTarefa()
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
            var useCase = new UseCase.CreateTarefa(
                repositoryMock.Object,
                unitOfWorkMock.Object
            );
            var input = _fixture.GetInput();
            var output = await useCase.Handle(input, CancellationToken.None);

            repositoryMock.Verify(
                repository => repository.Insert(
                    It.IsAny<DomainEntity.Tarefa>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once()
            );
            unitOfWorkMock.Verify(
                uow => uow.Commit(
                    It.IsAny<CancellationToken>()
                )
            );

            output.Should().NotBeNull();
            output.Titulo.Should().Be(input.Titulo);
            output.Descricao.Should().Be(input.Descricao);
            output.Status.Should().Be(input.Status);
            output.Id.Should().NotBeEmpty();
            output.DataVencimento.Should().NotBeSameDateAs(default);            
        }

        [Fact(DisplayName = nameof(CreateTarefaWithOnlyTitulo))]
        [Trait("Application", "CreateTarefa - Use Cases")]
        public async void CreateTarefaWithOnlyTitulo()
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
            var useCase = new UseCase.CreateTarefa(
                repositoryMock.Object,
                unitOfWorkMock.Object
            );
            var input = new CreateTarefaInput(
                _fixture.GetValidTarefaTitulo()
            );
            var output = await useCase.Handle(input, CancellationToken.None);

            repositoryMock.Verify(
                repository => repository.Insert(
                    It.IsAny<DomainEntity.Tarefa>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once()
            );
            unitOfWorkMock.Verify(
                uow => uow.Commit(
                    It.IsAny<CancellationToken>()
                )
            );

            output.Should().NotBeNull();
            output.Titulo.Should().Be(input.Titulo);
            output.Descricao.Should().Be("");
            output.Status.Should().Be(input.Status);
            output.Id.Should().NotBeEmpty();
            output.DataVencimento.Should().NotBeSameDateAs(default);            
        }

        [Fact(DisplayName = nameof(CreateTarefaWithOnlyTituloAndDescription))]
        [Trait("Application", "CreateTarefa - Use Cases")]
        public async void CreateTarefaWithOnlyTituloAndDescription()
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
            var useCase = new UseCase.CreateTarefa(
                repositoryMock.Object,
                unitOfWorkMock.Object
            );
            var input = new CreateTarefaInput(
                _fixture.GetValidTarefaTitulo(),
                _fixture.GetValidTarefaDescricao()
            );
            var output = await useCase.Handle(input, CancellationToken.None);

            repositoryMock.Verify(
                repository => repository.Insert(
                    It.IsAny<DomainEntity.Tarefa>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once()
            );
            unitOfWorkMock.Verify(
                uow => uow.Commit(
                    It.IsAny<CancellationToken>()
                )
            );

            output.Should().NotBeNull();
            output.Titulo.Should().Be(input.Titulo);
            output.Descricao.Should().Be(input.Descricao);
            output.Status.Should().Be(input.Status);
            output.Id.Should().NotBeEmpty();
            output.DataVencimento.Should().NotBeSameDateAs(default);
        }

        [Theory(DisplayName = nameof(ThrowWhenCantInstantiateTarefa))]
        [Trait("Application", "CreateTarefa - Use Cases")]
        [MemberData(
            nameof(CreateTarefaTestDataGenerator.GetInvalidInputs),
            parameters: 12,
            MemberType = typeof(CreateTarefaTestDataGenerator)
        )]
        public async void ThrowWhenCantInstantiateTarefa(
            CreateTarefaInput input,
            string exceptionMessage
        )
        {
            var useCase = new UseCase.CreateTarefa(
                _fixture.GetRepositoryMock().Object,
                _fixture.GetUnitOfWorkMock().Object
            );

            Func<Task> task =
                async () => await useCase.Handle(input, CancellationToken.None);

            await task.Should()
                .ThrowAsync<EntityValidationException>()
                .WithMessage(exceptionMessage);
        }
    }
}
