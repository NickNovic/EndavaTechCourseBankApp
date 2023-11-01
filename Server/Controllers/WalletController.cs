using EndavaTechCourseBankApp.Application.Queries.GetWallets;
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
        private readonly IMediator mediator;
        public WalletController(ApplicationDbContext dbContext)
        {
            ArgumentNullException.ThrowIfNull(dbContext);
            ArgumentNullException.ThrowIfNull(mediator);
            
            _context = dbContext;
            this.mediator = mediator;
        }

        [HttpPost]
        public IActionResult CreateWallet([FromBody] CreateWalletDTO createWalletDTO)
        {
            var wallet = new Wallet
            {
                Type = createWalletDTO.Type,
                Amount = createWalletDTO.Amount,
                Currency = new Currency {
                    ChangeRate = createWalletDTO.Currency.ChangeRate,
                    CurrencyCode = createWalletDTO.Currency.CurrencyCode,
                    Name = createWalletDTO.Currency.Name
                }
                
            };
            _context.wallets.Add(wallet);
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet]
        [Route("getwallets")]
        public async Task<List<GetWalletDTO>> GetWallets()
        {
           List<GetWalletDTO> walletsRes = new List<GetWalletDTO>();
            var query = new GetWalletsQuery();
            var wallets = await mediator.Send(query);

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
            Wallet w = await _context.wallets.FindAsync(id);
            Guid Cid = w.CurrencyId;
            w.Currency = await GetCurrencyById(Cid);
            return new GetWalletDTO {
                WalletId = w.Id,
                Amount = w.Amount,
                Currency = w.Currency,
                Pincode = w.Pincode,
                LastActivity = w.LastActivity,
                Type = w.Type
            };
        }

        [HttpGet]
        [Route("getCurrencyById")]
        public async Task<Currency> GetCurrencyById(Guid id)
        {
            return await _context.currencies.FindAsync(id);
        }
    }

}
