﻿using EndavaTechCourseBankApp.Domain.Models;
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

            
            string wCode = "";
            do
            {
                wCode = "";
                for (int i = 0; i < 4; i++)
                {
                    wCode += rnd.Next(1000, 9999).ToString();
                }
            } while (context.wallets.FirstOrDefault(w => w.Code == wCode) != null);

            var wallet = new Wallet
            {
                UserId = request.UserId,
                Type = request.Type,
                Amount = request.Amount,
                CurrencyId = currency.Id,
                Code = wCode                
            };
            
            await context.wallets.AddAsync(wallet);
            context.SaveChanges();
            return new CommandStatus();
        }
    }
}
