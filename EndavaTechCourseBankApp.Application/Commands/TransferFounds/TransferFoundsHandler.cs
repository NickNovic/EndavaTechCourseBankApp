using EndavaTechCourseBankApp.Domain.Models;
using EndavaTechCourseBankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndavaTechCourseBankApp.Application.Commands.TransferFounds
{
    public class TransferFoundsHandler : IRequestHandler<TransferFoundsCommand, CommandStatus>
    {
        private readonly ApplicationDbContext context;
        public TransferFoundsHandler(ApplicationDbContext context) 
        {
            this.context = context;
        }

        public async Task<CommandStatus> Handle(TransferFoundsCommand request, CancellationToken cancellationToken)
        {
            if(request.Amount < 0) 
            {
                return new CommandStatus(){ IsSuccessful = false, Error = "Amount is invalid" };
            }
            if(request == null) 
            {
                return new CommandStatus() { IsSuccessful = false };
            }

            Wallet sender = await context.wallets.FirstOrDefaultAsync(w => w.Code == request.CodeOfSender);
            Wallet accepter = await context.wallets.FirstOrDefaultAsync(w => w.Code == request.CodeOfAccepter);

            Currency senderCurrency = await context.currencies.FirstOrDefaultAsync(c => c.Id == sender.CurrencyId);
            Currency accepterCurrency = await context.currencies.FirstOrDefaultAsync(c => c.Id == accepter.CurrencyId);

            var walletType = this.context.commisions.FirstOrDefault(c => c.Type == sender.Type);
            float commision = 0;
            if (walletType is not null)
                commision = walletType.Percent;
            else
                return CommandStatus.Failed("no shuch walletType");

            var transferCurrency = await context.currencies.FirstOrDefaultAsync(c => c.Id == request.CurrencyId);

            if (senderCurrency == transferCurrency)
            {
                sender.Amount -= (decimal)((float)request.Amount * (1 + commision));
            }
            else
            {
                sender.Amount -= (decimal)((float)request.Amount * (1 + commision) * (float)transferCurrency.ChangeRate);
            }

            if(sender.Amount < 0)
            {
                return CommandStatus.Failed("Not enought money");
            }

            if(accepterCurrency == transferCurrency) 
            {
                accepter.Amount += request.Amount;
            }
            else
            {
                accepter.Amount = accepter.Amount + (request.Amount * transferCurrency.ChangeRate);
            }
                        
            context.wallets.Update(sender);
            context.wallets.Update(accepter);
            
            var transaction = new Transaction()
            {
                Currency = transferCurrency,
                Amount = request.Amount,
                ChangeRate = transferCurrency.ChangeRate,
                Description = request.Description, 
                Date = DateTime.Now,
                CodeOfSender = sender.Code,
                CodeOfAccepter = accepter.Code,
            };
            
            context.transactions.Add(transaction);
            context.SaveChanges();

            return new CommandStatus() { IsSuccessful = true};
        }
    }
}
