using Desafio.SisGerTarefas.Application.UseCases.Tarefa.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using FluentAssertions;
using Desafio.SisGerTarefas.Application.UseCases.Tarefa.CreateTarefa;

namespace Desafio.SisGerTarefas.EndTEndTests.Api.Tarefa.CreateTarefa
{
    [Collection(nameof(CreateTarefaApiTestFixture))]
    public class CreateTarefaApiTest
    {
        private readonly CreateTarefaApiTestFixture _fixture;

        public CreateTarefaApiTest(CreateTarefaApiTestFixture fixture)
            => _fixture = fixture;

        [Fact(DisplayName = nameof(CreateTarefa))]
        [Trait("EndToEnd/API", "Tarefa - Endpoints")]
        public async Task CreateTarefa()
        {
            var input = _fixture.GetExampleInput();

            var (response, output) = await _fixture
                .ApiClient.Post<TarefaModelOutput>(
                    "/tarefas",
                    input
                );

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be(HttpStatusCode.Created);
            output.Should().NotBeNull();
            output!.Titulo.Should().Be(input.Titulo);
            output.Descricao.Should().Be(input.Descricao);
            output.Status.Should().Be(input.Status);
            output.Id.Should().NotBeEmpty();
            output.DataVencimento.Should()
                .NotBeSameDateAs(default);
            var dbTarefa = await _fixture
                .Persistence.GetById(output.Id);
            dbTarefa.Should().NotBeNull();
            dbTarefa!.Titulo.Should().Be(input.Titulo);
            dbTarefa.Descricao.Should().Be(input.Descricao);
            dbTarefa.Status.Should().Be(input.Status);
            dbTarefa.Id.Should().NotBeEmpty();
            output.DataVencimento.Should()
                .NotBeSameDateAs(default);
        }

        [Theory(DisplayName = nameof(ThrowWhenCantInstantiateAggregate))]
        [Trait("EndToEnd/API", "Tarefa - Endpoints")]
        [MemberData(
            nameof(CreateTarefaApiTestDataGenerator.GetInvalidInputs),
            MemberType = typeof(CreateTarefaApiTestDataGenerator)
        )]
        public async Task ThrowWhenCantInstantiateAggregate(
            CreateTarefaInput input,
            string expectedDetail
        )
        {
            var (response, output) = await _fixture
                .ApiClient.Post<ProblemDetails>(
                    "/tarefas",
                    input
                );

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            output.Should().NotBeNull();
            output!.Title.Should().Be("One or more validation errors ocurred");
            output.Type.Should().Be("UnprocessableEntity");
            output.Status.Should().Be(StatusCodes.Status422UnprocessableEntity);
            output.Detail.Should().Be(expectedDetail);
        }
    }
}
