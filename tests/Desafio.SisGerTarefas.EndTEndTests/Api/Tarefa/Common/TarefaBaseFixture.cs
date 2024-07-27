using Desafio.SisGerTarefas.EndTEndTests.Base;

namespace Desafio.SisGerTarefas.EndTEndTests.Api.Tarefa.Common
{
    public class TarefaBaseFixture
        :BaseFixture
    {
        public TarefaPersistence Persistence;

        public TarefaBaseFixture()
            : base()
        {
            Persistence = new TarefaPersistence(
                CreateDbContext()
            );
        }

        public string GetValidTarefaTitulo()
        {
            var tarefaTitulo = "";

            while (tarefaTitulo.Length < 3)
                tarefaTitulo = Faker.Commerce.Categories(1)[0];

            if (tarefaTitulo.Length > 255)
                tarefaTitulo = tarefaTitulo[..255];

            return tarefaTitulo;
        }

        public string GetValidTarefaDescription()
        {
            var tarefaDescription =
                Faker.Commerce.ProductDescription();

            if (tarefaDescription.Length > 10_000)
                tarefaDescription
                    = tarefaDescription[..10_000];

            return tarefaDescription;
        }       

        public string GetInvalidTituloTooShort()
            => Faker.Commerce.ProductName().Substring(0, 2);

        public string GetInvalidTituloTooLong()
        {
            var tooLongTituloForTarefa = Faker.Commerce.ProductName();
            while (tooLongTituloForTarefa.Length <= 255)
                tooLongTituloForTarefa = $"{tooLongTituloForTarefa} {Faker.Commerce.ProductName()}";
            return tooLongTituloForTarefa;
        }

        public string GetInvalidDescriptionTooLong()
        {
            var tooLongDescriptionForTarefa = Faker.Commerce.ProductDescription();
            while (tooLongDescriptionForTarefa.Length <= 10_000)
                tooLongDescriptionForTarefa = $"{tooLongDescriptionForTarefa} {Faker.Commerce.ProductDescription()}";
            return tooLongDescriptionForTarefa;
        }
    }
}
