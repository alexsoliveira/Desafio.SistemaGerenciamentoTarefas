using Desafio.SisGerTarefas.Domain.Exceptions;
using FluentAssertions;
using DomainEntity = Desafio.SisGerTarefas.Domain.Entity;

namespace Desafio.SisGerTarefas.UnitTests.Domain.Entity.Tarefa
{
    [Collection(nameof(TarefaTestFixture))]
    public class TarefaTest
    {
        private readonly TarefaTestFixture _tarefaTestFixture;

        public TarefaTest(TarefaTestFixture tarefaTestFixture)
            => _tarefaTestFixture = tarefaTestFixture;

        [Fact(DisplayName = nameof(Instantiate))]
        [Trait("Domain", "Tarefa - Aggregates")]
        public void Instantiate()
        {
            var validTarefa = _tarefaTestFixture.GetValidTarefa();
            var datetimeBefore = DateTime.Now;

            var tarefa = new DomainEntity.Tarefa(
                validTarefa.IdUsuario.ToString(), 
                validTarefa.Titulo, 
                validTarefa.Descricao);
            var datetimeAfter = DateTime.Now.AddSeconds(1);

            tarefa.Should().NotBeNull();
            tarefa.Id.Should().NotBeEmpty();
            tarefa.IdUsuario.Should().NotBeEmpty();
            tarefa.IdUsuario.Should().Be(validTarefa.IdUsuario);
            tarefa.Titulo.Should().Be(validTarefa.Titulo);
            tarefa.Descricao.Should().Be(validTarefa.Descricao);            
            tarefa.DataVencimento.Should().NotBeSameDateAs(default(DateTime));
            (tarefa.DataVencimento >= datetimeBefore).Should().BeTrue();
            (tarefa.DataVencimento <= datetimeAfter).Should().BeTrue();
            tarefa.Status.Should().Be(DomainEntity.Status.Pendente);            
        }

        [Theory(DisplayName = nameof(InstantiateErrorWhenIdUsuarioIsEmpty))]
        [Trait("Domain", "Tarefa - Aggregates")]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]        
        public void InstantiateErrorWhenIdUsuarioIsEmpty(string? idUsuario)
        {
            var validTarefa = _tarefaTestFixture.GetValidTarefa();

            Action action =
                () => new DomainEntity.Tarefa(
                    idUsuario!, validTarefa.Titulo, validTarefa.Descricao);

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("IdUsuario should not be empty or null");
        }

        [Theory(DisplayName = nameof(InstantiateErrorWhenTituloIsEmpty))]
        [Trait("Domain", "Tarefa - Aggregates")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public void InstantiateErrorWhenTituloIsEmpty(string? titulo)
        {
            var validTarefa = _tarefaTestFixture.GetValidTarefa();

            Action action =
                () => new DomainEntity.Tarefa(
                    validTarefa.IdUsuario.ToString(), titulo!, validTarefa.Descricao);

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Titulo should not be empty or null");            
        }

        [Fact(DisplayName = nameof(InstantiateErrorWhenDescricaoIsNull))]
        [Trait("Domain", "Tarefa - Aggregates")]
        public void InstantiateErrorWhenDescricaoIsNull()
        {
            var validTarefa = _tarefaTestFixture.GetValidTarefa();

            Action action =
                () => new DomainEntity.Tarefa(
                    validTarefa.IdUsuario.ToString(), validTarefa.Titulo, null!);

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Descricao should not be null");           
        }

        [Theory(DisplayName = nameof(InstantiateErrorWhenTituloIsLessThan3Characters))]
        [Trait("Domain", "Tarefa - Aggregates")]
        [MemberData(nameof(GetTituloWithLessThan3Caracters), parameters: 10)]
        public void InstantiateErrorWhenTituloIsLessThan3Characters(string invalidTitulo)
        {
            var validTarefa = _tarefaTestFixture.GetValidTarefa();

            Action action =
                () => new DomainEntity.Tarefa(
                    validTarefa.IdUsuario.ToString(), invalidTitulo, validTarefa.Descricao);

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Titulo should be at leats 3 characters long");            
        }

        public static IEnumerable<object[]> GetTituloWithLessThan3Caracters(int numberOfTests = 6)
        {
            var fixture = new TarefaTestFixture();

            for (int i = 0; i < numberOfTests; i++)
            {
                var isOdd = i % 2 == 1;
                yield return new object[] {
                    fixture.GetValidTarefaTitulo()[ ..(isOdd ? 1 : 2)]
                };
            }
        }

        [Fact(DisplayName = nameof(InstantiateErrorWhenTituloIsGreaterThan255Characters))]
        [Trait("Domain", "Tarefa - Aggregates")]
        public void InstantiateErrorWhenTituloIsGreaterThan255Characters()
        {
            var validTarefa = _tarefaTestFixture.GetValidTarefa();
            var invalidTitulo = string.Join(null, Enumerable.Range(1, 256).Select(_ => "a").ToArray());

            Action action =
                () => new DomainEntity.Tarefa(
                    validTarefa.IdUsuario.ToString(), invalidTitulo, validTarefa.Descricao);

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Titulo should be less or equal 255 characters long");
        }

        [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsGreaterThan10_000Characters))]
        [Trait("Domain", "Tarefa - Aggregates")]
        public void InstantiateErrorWhenDescriptionIsGreaterThan10_000Characters()
        {
            var validTarefa = _tarefaTestFixture.GetValidTarefa();
            var invalidDescription = string.Join(null, Enumerable.Range(1, 10_001).Select(_ => "a").ToArray());

            Action action =
                () => new DomainEntity.Tarefa(
                    validTarefa.IdUsuario.ToString(), validTarefa.Titulo, invalidDescription);

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Descricao should be less or equal 10000 characters long");
        }

        [Fact(DisplayName = nameof(Atualizar))]
        [Trait("Domain", "Tarefa - Aggregates")]
        public void Atualizar()
        {
            var validTarefa = _tarefaTestFixture.GetValidTarefa();
            var tarefaWithNewValue = _tarefaTestFixture.GetValidTarefa();
            var newData = DateTime.Now;
            var newStatus = DomainEntity.Status.Concluido;

            validTarefa.Update(
                tarefaWithNewValue.IdUsuario.ToString(),
                tarefaWithNewValue.Titulo, 
                descricao: tarefaWithNewValue.Descricao, 
                data: newData,
            status: newStatus);

            validTarefa.IdUsuario.Should().NotBeEmpty();
            validTarefa.IdUsuario.Should().Be(tarefaWithNewValue.IdUsuario);
            validTarefa.Titulo.Should().Be(tarefaWithNewValue.Titulo);
            validTarefa.Descricao.Should().Be(tarefaWithNewValue.Descricao);
            validTarefa.DataVencimento.Should().Be(newData);
            validTarefa.Status.Should().Be(newStatus);
        }

        [Fact(DisplayName = nameof(AtualizarApenasTitulo))]
        [Trait("Domain", "Tarefa - Aggregates")]
        public void AtualizarApenasTitulo()
        {
            var tarefaValida = _tarefaTestFixture.GetValidTarefa();

            var novoTitulo = _tarefaTestFixture.GetValidTarefaTitulo();
            var idUsuariAtual = tarefaValida.IdUsuario.ToString();
            var descricaoAtual = tarefaValida.Descricao;

            tarefaValida.Update(idUsuariAtual, novoTitulo);

            tarefaValida.IdUsuario.Should().Be(idUsuariAtual);
            tarefaValida.Titulo.Should().Be(novoTitulo);
            tarefaValida.Descricao.Should().Be(descricaoAtual);            
        }

        [Theory(DisplayName = nameof(UpdateErrorWhenTituloIsEmpty))]
        [Trait("Domain", "Tarefa - Aggregates")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public void UpdateErrorWhenTituloIsEmpty(string titulo)
        {
            var tarefaValida = _tarefaTestFixture.GetValidTarefa();           

            Action action =
                () => tarefaValida.Update(tarefaValida.IdUsuario.ToString(), titulo!);

            action.Should().Throw<EntityValidationException>()
                .WithMessage("Titulo should not be empty or null");            
        }

        [Theory(DisplayName = nameof(UpdateErrorWhenTituloIsLessThan3Characters))]
        [Trait("Domain", "Tarefa - Aggregates")]
        [InlineData("1")]
        [InlineData("12")]
        [InlineData("a")]
        [InlineData("ab")]
        public void UpdateErrorWhenTituloIsLessThan3Characters(string invalidTitulo)
        {
            var tarefaValida = _tarefaTestFixture.GetValidTarefa();            

            Action action =
                () => tarefaValida.Update(
                    tarefaValida.IdUsuario.ToString(), invalidTitulo, tarefaValida.Descricao);

            action.Should().Throw<EntityValidationException>()
                .WithMessage("Titulo should be at leats 3 characters long");     
        }

        [Fact(DisplayName = nameof(UpdateErrorWhenTituloIsGreaterThan255Characters))]
        [Trait("Domain", "Tarefa - Aggregates")]
        public void UpdateErrorWhenTituloIsGreaterThan255Characters()
        {
            var tarefaValida = _tarefaTestFixture.GetValidTarefa();
            var invalidTitulo = _tarefaTestFixture.Faker.Lorem.Letter(256);            

            Action action =
                () => tarefaValida.Update(
                    tarefaValida.IdUsuario.ToString(), invalidTitulo, tarefaValida.Descricao);

            action.Should().Throw<EntityValidationException>()
               .WithMessage("Titulo should be less or equal 255 characters long");            
        }

        [Fact(DisplayName = nameof(UpdateErrorWhenDescricaoIsGreaterThan10_000Characters))]
        [Trait("Domain", "Tarefa - Aggregates")]
        public void UpdateErrorWhenDescricaoIsGreaterThan10_000Characters()
        {
            var tarefaValida = _tarefaTestFixture.GetValidTarefa();
            var invalidDescription = _tarefaTestFixture.Faker.Commerce.ProductDescription();            

            while (invalidDescription.Length <= 10_000)
                invalidDescription = $"{invalidDescription} {_tarefaTestFixture.Faker.Commerce.ProductDescription()}";

            Action action =
                () => new DomainEntity.Tarefa(
                    tarefaValida.IdUsuario.ToString(), tarefaValida.Titulo, invalidDescription);

            action.Should().Throw<EntityValidationException>()
               .WithMessage("Descricao should be less or equal 10000 characters long");
        }
    }
}
