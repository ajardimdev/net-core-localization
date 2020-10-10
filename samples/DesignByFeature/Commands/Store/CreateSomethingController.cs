using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;

namespace DesignByFeature.Commands.Store
{
    /// <summary>
    /// Entrada de ações referentes a Usuários
    /// </summary>
    [ApiController]
    [Route("api/something")]
    public class CreateSomethingController : Controller
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Gerenciador de login
        /// </summary>
        /// <param name="mediator">Pipeline commands mediator</param>
        public CreateSomethingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Store([FromBody] StoreSomething request)
        {
            var response = await _mediator.Send(request);

            if (response.ValidationResult != null)
                return BadRequest(response);

            return Ok(response);
        }
    }
}