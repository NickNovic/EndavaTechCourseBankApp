using EndavaTechCourseBankApp.Application.Commands.AddCurrency;
using EndavaTechCourseBankApp.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EndavaTechCourseBankApp.Server.Controllers
{
    [Route("api/currencyes")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly IMediator mediator;

        public CurrencyController(IMediator mediator)
        {
            ArgumentNullException.ThrowIfNull(mediator);
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddCurrency([FromBody]CurrencyDTO currencyDTO)
        {
            var command = new AddCurrencyCommand
            {
                Name = currencyDTO.Name,
                CurrencyCode = currencyDTO.CurrencyCode,
                ChangeRate = currencyDTO.ChangeRate
            };
            var result = await mediator.Send(command);
            
            return result.IsSuccessful ? Ok() : BadRequest(result.Error);
        }
    }
}
