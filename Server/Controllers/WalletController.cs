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
using EndavaTechCourseBankApp.Application.Commands.TransferFounds;
using System.Diagnostics;
using EndavaTechCourseBankApp.Application.Queries.GetTransactions;
using Microsoft.IdentityModel.Tokens;

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
                CurrencyCode = createWalletDTO.CurrencyCode,
                UserId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(u => u.Type == Constants.UserIdClaimName).Value)
            };
            
            Debug.WriteLine("here");

            await _mediator.Send(query);
            return Ok();
        }

        [HttpGet]
        [Route("getwallets")]
        [Authorize(Roles = "User, Admin")]
        public async Task<List<GetWalletDTO>> GetWallets()
        {
            List<GetWalletDTO> walletsRes = new List<GetWalletDTO>();
            var query = new GetWalletsQuery 
            { 
                UserId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == Constants.UserIdClaimName).Value)
            };
            var wallets = await _mediator.Send(query);

            foreach (Wallet w in wallets)
            {
                var getCurrencyForDTO = new GetCurrencyByIdQuery()
                {
                    Id = w.CurrencyId
                };
                var currency = await _mediator.Send(getCurrencyForDTO);

                walletsRes.Add(new GetWalletDTO
                {
                    CurrencyId = currency.Id,
                    WalletId = w.Id,
                    Amount = w.Amount,
                    CurrencyCode = currency.CurrencyCode,
                    ChangeRate = currency.ChangeRate,
                    CurrencyName = currency.Name,
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

        [HttpPost]
        [Authorize(Roles = "User, Admin")]
        [Route("transfer")]
        public async Task<IActionResult> TranserFounds([FromBody] TransferDTO transfer)  
        {
            var query = new TransferFoundsCommand()
            {
                Amount = transfer.Amount,
                IdOfAccepter = transfer.IdOfAccepter,
                Description = transfer.Description,
                IdOfSender = transfer.IdOfSender,
                CurrencyId = transfer.CurrencyId,
                Date = DateTime.Now
            };
            var res = await _mediator.Send(query);

            return res.IsSuccessful ? Ok() : BadRequest();
        }

        [HttpGet]
        [Authorize(Roles = "User, Admin")]
        [Route("gettransactions")]
        public async Task<IActionResult> GetTransactions() 
        {
            var query = new GetTransactionsQuery() 
            {
                UserId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == Constants.UserIdClaimName).Value),
            };

            var res = await _mediator.Send(query);
            
            if(res.IsNullOrEmpty())
            {
                return BadRequest();
            }
            
            return Ok(res);
        }
    }
}