using EndavaTechCourseBankApp.Domain.Models;
using EndavaTechCourseBankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndavaTechCourseBankApp.Application.Commands.DeleteCurrencyById
{
    public class DeleteCurrencyByIdHandler : IRequestHandler<DeleteCurrencyByIdCommand, CommandStatus>
    {
        protected readonly ApplicationDbContext context;
        
        public DeleteCurrencyByIdHandler(ApplicationDbContext context) 
        {
            this.context = context;
        }

        public async Task<CommandStatus> Handle(DeleteCurrencyByIdCommand request, CancellationToken cancellationToken)
        {
            var currency = context.currencies.FirstOrDefault(t => t.Id == request.Id);
            context.currencies.Remove(currency);

            return new CommandStatus { IsSuccessful = true };
        }
    }
}
