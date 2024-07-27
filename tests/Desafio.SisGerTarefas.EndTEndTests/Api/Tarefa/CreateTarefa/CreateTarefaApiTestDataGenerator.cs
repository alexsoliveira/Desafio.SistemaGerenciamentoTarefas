namespace Desafio.SisGerTarefas.EndTEndTests.Api.Tarefa.CreateTarefa
{
    public class CreateTarefaApiTestDataGenerator
    {
        public static IEnumerable<object[]> GetInvalidInputs()
        {
            var fixture = new CreateTarefaApiTestFixture();
            var invalidInputsList = new List<object[]>();
            var totalInvalidCases = 3;

            for (int index = 0; index < totalInvalidCases; index++)
            {
                switch (index % totalInvalidCases)
                {
                    case 0:
                        var input1 = fixture.GetExampleInput();
                        input1.Titulo = fixture.GetInvalidTituloTooShort();
                        invalidInputsList.Add(new object[] {
                            input1,
                            "Titulo should be at leats 3 characters long"
                        });
                        break;
                    case 1:
                        var input2 = fixture.GetExampleInput();
                        input2.Titulo = fixture.GetInvalidTituloTooLong();
                        invalidInputsList.Add(new object[] {
                            input2,
                            "Titulo should be less or equal 255 characters long"
                    });
                        break;
                    case 2:
                        var input3 = fixture.GetExampleInput();
                        input3.Descricao = fixture.GetInvalidDescriptionTooLong();
                        invalidInputsList.Add(new object[] {
                            input3,
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
