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

            Wallet sender = await context.wallets.FirstOrDefaultAsync(w => w.Id == request.IdOfSender);
            Wallet accepter = await context.wallets.FirstOrDefaultAsync(w => w.Id == request.IdOfAccepter);

            Currency senderCurrency = await context.currencies.FirstOrDefaultAsync(c => c.Id == sender.CurrencyId);
            Currency accepterCurrency = await context.currencies.FirstOrDefaultAsync(c => c.Id == accepter.CurrencyId);
            

            var transferCurrency = await context.currencies.FirstOrDefaultAsync(c => c.Id == request.CurrencyId);

            if (senderCurrency == transferCurrency)
            {
                sender.Amount -= request.Amount;
            }
            else
            {
                sender.Amount = sender.Amount - (request.Amount * transferCurrency.ChangeRate);
            }

            if(accepterCurrency == transferCurrency) 
            {
                accepter.Amount -= request.Amount;
            }
            else
            {
                accepter.Amount = accepter.Amount - (request.Amount * transferCurrency.ChangeRate);
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
                IdOfSender = sender.Id,
                IdOfAccepter = accepter.Id,
            };
            
            context.transactions.Add(transaction);
            context.SaveChanges();

            return new CommandStatus() { IsSuccessful = true};
        }
    }
}
