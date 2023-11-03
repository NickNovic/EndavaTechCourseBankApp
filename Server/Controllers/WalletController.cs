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
        public async Task CreateWallet([FromBody] CreateWalletDTO createWalletDTO)
        {
            var query = new CreateWalletCommand
            {
                Type = createWalletDTO.Type,
                Amount = createWalletDTO.Amount,
                CurrencyCode = createWalletDTO.CurrencyCode
            };

            await _mediator.Send(query);
            
        }

        [HttpGet]
        [Route("getwallets")]
        public async Task<List<GetWalletDTO>> GetWallets()
        {
           List<GetWalletDTO> walletsRes = new List<GetWalletDTO>();
            var query = new GetWalletsQuery();
            var wallets = await _mediator.Send(query);

            foreach (Wallet w in wallets)
            {
                walletsRes.Add(new GetWalletDTO
                {
                    WalletId = w.Id,
                    Amount = w.Amount,
                    Currency = w.Currency,
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
                Currency = w.Currency,
                Pincode = w.Pincode,
                LastActivity = w.LastActivity,
                Type = w.Type
            };

        }

        
    }

}
