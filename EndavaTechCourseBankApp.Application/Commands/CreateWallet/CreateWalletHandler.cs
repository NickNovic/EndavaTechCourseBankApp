using EndavaTechCourseBankApp.Domain.Models;
using EndavaTechCourseBankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            var wallet = new Wallet
            {
                UserId = request.UserId,
                Type = request.Type,
                Amount = request.Amount,
                CurrencyId = currency.Id
            };
            
            
            await context.wallets.AddAsync(wallet);
            context.SaveChanges();
            return new CommandStatus();
        }
    }
}
