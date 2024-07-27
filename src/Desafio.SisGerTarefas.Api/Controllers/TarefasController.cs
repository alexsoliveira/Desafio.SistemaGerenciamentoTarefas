using Desafio.SisGerTarefas.Application.UseCases.Tarefa.Common;
using Desafio.SisGerTarefas.Application.UseCases.Tarefa.CreateTarefa;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.SisGerTarefas.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TarefasController(IMediator mediator)
            => _mediator = mediator;

        [HttpPost]
        [ProducesResponseType(typeof(TarefaModelOutput), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Create(
            [FromBody] CreateTarefaInput input,
            CancellationToken cancellationToken
        )
        {
            var output = await _mediator.Send(input, cancellationToken);
            return CreatedAtAction(
                nameof(Create), new { output.Id }, output);
        }
    }
}
