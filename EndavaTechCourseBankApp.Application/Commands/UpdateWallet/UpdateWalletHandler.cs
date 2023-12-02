using EndavaTechCourseBankApp.Domain.Enums;
using EndavaTechCourseBankApp.Domain.Models;
using EndavaTechCourseBankApp.Infrastructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndavaTechCourseBankApp.Application.Commands.UpdateWallet
{
    public class UpdateWalletHandler : IRequestHandler<UpdateWalletCommand, CommandStatus>
    {
        private readonly ApplicationDbContext context;
        public UpdateWalletHandler(ApplicationDbContext context)
        {
            this.context = context;
        }
        
        public async Task<CommandStatus> Handle(UpdateWalletCommand request, CancellationToken cancellationToken)
        {
            Wallet wallet = await context.wallets.FindAsync(request.WalletId);
            if(wallet == null) 
            {
                return new CommandStatus() { IsSuccessful  = false, Error = "There is no wallet with this " };
            }

            wallet.Amount = request.Amount;
            wallet.Pincode = request.Pincode;
            wallet.CurrencyId = request.CurrencyId;
            wallet.LastActivity = request.LastActivity;
            wallet.Type = (WalletType)Enum.Parse(typeof(WalletType), request.Type);
           

            var res = context.wallets.Update(wallet);
            context.SaveChanges();
            
            return new CommandStatus();
        }
    }
}
