using EndavaTechCourseBankApp.Domain.Models;
using EndavaTechCourseBankApp.Infrastructure.Persistence;
using EndavaTechCourseBankApp.Shared;
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

        public WalletController(ApplicationDbContext dbContext)
        {
            ArgumentNullException.ThrowIfNull(dbContext);
            _context = dbContext;
        }

        [HttpPost]
        public IActionResult CreateWallet([FromBody] CreateWalletDTO createWalletDTO)
        {
            var wallet = new Wallet
            {
                Type = createWalletDTO.Type,
                Amount = createWalletDTO.Amount,
                Currency = createWalletDTO.Currency
                
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
            List<Wallet> wallets = await _context.wallets.Include(c => c.Currency).ToListAsync();
            foreach(Wallet w in wallets)
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
