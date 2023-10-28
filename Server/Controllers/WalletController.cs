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
        public async Task<List<Wallet>> GetWallets()
        {
            return await _context.wallets.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<Wallet> GetWalletById(Guid id)
        {
            return await _context.wallets.FindAsync(id);
        }
    }

}
