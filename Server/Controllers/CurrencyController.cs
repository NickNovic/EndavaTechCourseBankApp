using EndavaTechCourseBankApp.Application.Commands.AddCurrency;
using EndavaTechCourseBankApp.Application.Commands.DeleteCurrencyById;
using EndavaTechCourseBankApp.Application.Commands.UpdateCurrency;
using EndavaTechCourseBankApp.Application.Queries.GetCurrencies;
using EndavaTechCourseBankApp.Application.Queries.GetCurrencyById;
using EndavaTechCourseBankApp.Domain.Models;
using EndavaTechCourseBankApp.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult<List<CurrencyDTO>>> GetCurrencies()
        {
            var currencyesRes = new List<CurrencyDTO>();

            var query = new GetCurrenciesQuery();
            var result = await mediator.Send(query);
            if (result == null)
                return BadRequest("result is null");

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
        [Authorize(Roles = "Admin")]
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
        public async Task<CurrencyDTO> GetCurrencyById(Guid id)
        {
            GetCurrencyByIdQuery request = new ()
            {
                Id = id
            };
            var res = await mediator.Send(request);
            return new CurrencyDTO
            {
                Id = res.Id.ToString(),
                CurrencyCode = res.CurrencyCode,
                ChangeRate = res.ChangeRate,
                Name = res.Name
            };
        }

        [HttpPost]
        [Route("delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCurrencyById([FromBody]Guid id)
        {
            DeleteCurrencyByIdCommand request = new() { Id = id };

            var res = await mediator.Send(request);

            if (res.IsSuccessful && res != null)
                return Ok();
            return BadRequest();
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
