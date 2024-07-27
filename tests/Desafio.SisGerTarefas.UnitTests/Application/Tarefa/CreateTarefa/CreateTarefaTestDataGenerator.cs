namespace Desafio.SisGerTarefas.UnitTests.Application.Tarefa.CreateTarefa
{
    public class CreateTarefaTestDataGenerator
    {
        public static IEnumerable<object[]> GetInvalidInputs(int times = 12)
        {
            var fixture = new CreateTarefaTestFixture();
            var invalidInputsList = new List<object[]>();
            var totalInvalidCases = 4;

            for (int index = 0; index < times; index++)
            {
                switch (index % totalInvalidCases)
                {
                    case 0:
                        invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputShortTitulo(),
                        "Titulo should be at leats 3 characters long"
                    });
                        break;
                    case 1:
                        invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputTooLongTitulo(),
                        "Titulo should be less or equal 255 characters long"
                    });
                        break;
                    case 2:
                        invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputDescriptionNull(),
                        "Descricao should not be null"
                    });
                        break;
                    case 3:
                        invalidInputsList.Add(new object[] {
                        fixture.GetInvalidInputTooLongDescription(),
                        "Descricao should be less or equal 10000 characters long"
                    });
                        break;
                    default:
                        break;
                }
            }

            return invalidInputsList;
        }
    }
}
