﻿using EndavaTechCourseBankApp.Infrastructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndavaTechCourseBankApp.Application.Commands.DeleteWalletById
{
    public class DeleteWalletByIdHandler : IRequestHandler<DeleteWalletByIdCommand, CommandStatus>
    {
        private readonly ApplicationDbContext context;
        
        public DeleteWalletByIdHandler(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<CommandStatus> Handle(DeleteWalletByIdCommand request, CancellationToken cancellationToken)
        {
            var wallet = context.wallets.FirstOrDefault(t => t.Id == request.Id);
            if(wallet == null) 
            {
                return new CommandStatus() { IsSuccessful = false, Error = "There is no wallet with this Id" };
            }

            context.wallets.Remove(wallet);
            context.SaveChanges();
            return new CommandStatus { IsSuccessful = true };
        }
    }
}
