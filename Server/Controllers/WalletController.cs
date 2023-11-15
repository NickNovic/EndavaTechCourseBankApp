using EndavaTechCourseBankApp.Application.Commands.CreateWallet;
using EndavaTechCourseBankApp.Application.Queries.GetWallets;
using EndavaTechCourseBankApp.Application.Queries.GetWalletsById;
using EndavaTechCourseBankApp.Application.Queries.GetCurrencyById;
using EndavaTechCourseBankApp.Domain.Models;
using EndavaTechCourseBankApp.Infrastructure.Persistence;
using EndavaTechCourseBankApp.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EndavaTechCourseBankApp.Application.Commands.DeleteWalletById;
using EndavaTechCourseBankApp.Application.Commands.UpdateWallet;
using Microsoft.AspNetCore.Authorization;
using EndavaTechCourseBankApp.Server.Common.JwtToken;

namespace EndavaTechCourseBankApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMediator _mediator;
        public WalletController(ApplicationDbContext dbContext, IMediator mediator)
        {
            ArgumentNullException.ThrowIfNull(dbContext);
            ArgumentNullException.ThrowIfNull(mediator);
            
            _context = dbContext;
            this._mediator = mediator;
        }

        
        [HttpPost]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> CreateWallet([FromBody] CreateWalletDTO createWalletDTO)
        {
            var query = new CreateWalletCommand
            {
                Type = createWalletDTO.Type,
                Amount = createWalletDTO.Amount,
                CurrencyCode = createWalletDTO.CurrencyCode
            };

            await _mediator.Send(query);
            return Ok();
        }

        [HttpGet]
        [Route("getwallets")]
        [Authorize(Roles = "User, Admin")]
        public async Task<List<GetWalletDTO>> GetWallets()
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == Constants.UserIdClaimName);

            if(userIdClaim == null)
            {
                return new List<GetWalletDTO>();
            }
            var userId = userIdClaim.Value;

            List<GetWalletDTO> walletsRes = new List<GetWalletDTO>();
            var query = new GetWalletsQuery();
            var wallets = await _mediator.Send(query);

            foreach (Wallet w in wallets)
            {
                walletsRes.Add(new GetWalletDTO
                {
                    WalletId = w.Id,
                    Amount = w.Amount,
                    CurrencyCode = w.Currency.CurrencyCode,
                    ChangeRate = w.Currency.ChangeRate,
                    CurrencyName = w.Currency.Name,
                    Pincode = w.Pincode,
                    LastActivity = w.LastActivity,
                    Type = w.Type
                });
            }
            return walletsRes;
        }

        [HttpGet("{id}")]
        [Route("getWalletById")]
        public async Task<GetWalletDTO> GetWalletById(Guid id)
        {
            GetWalletByIdQuery query = new GetWalletByIdQuery 
            { 
                Id = id 
            };
            var w = await _mediator.Send(query);

            return new GetWalletDTO {
                WalletId = w.Id,
                Amount = w.Amount,
                //Currency = w.Currency,
                Pincode = w.Pincode,
                LastActivity = w.LastActivity,
                Type = w.Type
            };
        }
        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> DeleteWalletById(Guid id)
        {
            DeleteWalletByIdCommand request = new DeleteWalletByIdCommand { Id = id };
            await _mediator.Send(request);

            return Ok();
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateWalletById([FromBody]UpdateWalletDTO walletDTO)
        {
            var currequest = new GetCurrencyByIdQuery()
            {
                Id = walletDTO.CurrencyId
            };
            var resCur = await _mediator.Send(currequest);

            var request = new UpdateWalletCommand() 
            {
                Amount = walletDTO.Amount,
                Currency = resCur,
                Pincode = walletDTO.Pincode,
                LastActivity = DateTime.Now,
                Type = walletDTO.Type,
                CurrencyId = walletDTO.CurrencyId,
                WalletId = walletDTO.WalletId
            };

            var res = await _mediator.Send(request);
            return res.IsSuccessful ? Ok() : BadRequest();
        }
    }
}
