using EndavaTechCourseBankApp.Application.Commands.AddCurrency;
using EndavaTechCourseBankApp.Application.Commands.DeleteCurrencyById;
using EndavaTechCourseBankApp.Application.Commands.UpdateCurrency;
using EndavaTechCourseBankApp.Application.Queries.GetCurrencies;
using EndavaTechCourseBankApp.Application.Queries.GetCurrencyById;
using EndavaTechCourseBankApp.Domain.Models;
using EndavaTechCourseBankApp.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EndavaTechCourseBankApp.Server.Controllers
{
    [Route("api/currencies")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly IMediator mediator;

        public CurrencyController(IMediator mediator)
        {
            ArgumentNullException.ThrowIfNull(mediator);
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<List<CurrencyDTO>> GetCurrencyes()
        {
            var currencyesRes = new List<CurrencyDTO>();

            var query = new GetCurrenciesQuery();
            var result = await mediator.Send(query);
            foreach (var c in result) {
                currencyesRes.Add(new CurrencyDTO
                {
                    CurrencyCode = c.CurrencyCode,
                    ChangeRate = c.ChangeRate,
                    Name = c.Name,
                    Id = c.Id.ToString(),
                    CanBeRemoved = true }
                );
            }

            return currencyesRes;
        }

        [HttpPost]
        public async Task<IActionResult> AddCurrency([FromBody] CurrencyDTO currencyDTO)
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

        [HttpGet]
        [Route("{id}")]
        public async Task<Currency> GetCurrencyById(Guid id)
        {
            GetCurrencyByIdQuery request = new GetCurrencyByIdQuery
            {
                Id = id
            };
            return await mediator.Send(request);
        }

        [HttpPost]
        [Route("{id}")]
        public async Task DeleteCurrencyById(Guid id)
        {
            DeleteCurrencyByIdCommand request = new DeleteCurrencyByIdCommand { Id = id };
            await mediator.Send(request);
        }

        [HttpPost]
        [Route("update")]
        public async Task<ActionResult> UpdateCurrency([FromBody]UpdateCurrencyDTO currencyDTO) 
        {
            var request = new UpdateCurrencyCommand() 
            {
                CurrencyId = currencyDTO.CurrencyId,
                CurrencyCode = currencyDTO.CurrencyCode,
                ChangeRate = currencyDTO.ChangeRate,
                Name = currencyDTO.Name
            };

            await mediator.Send(request);

            return Ok();
        }
    }
}
