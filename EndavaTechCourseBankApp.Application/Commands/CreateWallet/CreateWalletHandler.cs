using EndavaTechCourseBankApp.Domain.Models;
using EndavaTechCourseBankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndavaTechCourseBankApp.Application.Commands.CreateWallet
{
    public class CreateWalletHandler : IRequestHandler<CreateWalletCommand, CommandStatus>
    {
        private readonly ApplicationDbContext context;
        public CreateWalletHandler(ApplicationDbContext context)
        {
            this.context = context;
        }   

        public async Task<CommandStatus> Handle(CreateWalletCommand request, CancellationToken cancellationToken)
        {
            Currency currency = await context.currencies.FirstOrDefaultAsync(c => c.CurrencyCode == request.CurrencyCode);
            
            if(currency == null) 
            {
                return new CommandStatus { IsSuccessful = false, Error = "Currency does not exists" };
            }

            Wallet getWal;
            var rnd = new Random();
            var walletCode = new WalletCode();

            
            int code;
            do
            {
                code = rnd.Next(1000, 10000);
                getWal = context.wallets.FirstOrDefault(w => w.WalletCode.a == code);
            } while (getWal != null);
            walletCode.a = code;

            do
            {
                code = rnd.Next(1000, 10000);
                getWal = context.wallets.FirstOrDefault(w => w.WalletCode.b == code);
            } while (getWal != null);
            walletCode.b = code;

            do
            {
                code = rnd.Next(1000, 10000);
                getWal = context.wallets.FirstOrDefault(w => w.WalletCode.c == code);
            } while (getWal != null);
            walletCode.c = code;

            do
            {
                code = rnd.Next(1000, 10000);
                getWal = context.wallets.FirstOrDefault(w => w.WalletCode.d == code);
            } while (getWal != null);
            walletCode.d = code;


            var wallet = new Wallet
            {
                UserId = request.UserId,
                Type = request.Type,
                Amount = request.Amount,
                CurrencyId = currency.Id,
                WalletCode = walletCode                
            };
            
            await context.wallets.AddAsync(wallet);
            context.SaveChanges();
            return new CommandStatus();
        }
    }
}
