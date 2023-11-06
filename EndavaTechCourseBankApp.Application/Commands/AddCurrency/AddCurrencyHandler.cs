using EndavaTechCourseBankApp.Domain.Models;
using EndavaTechCourseBankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndavaTechCourseBankApp.Application.Commands.AddCurrency
{
    public class AddCurrencyHandler : IRequestHandler<AddCurrencyCommand, CommandStatus>
    {
        private readonly ApplicationDbContext context;
        public AddCurrencyHandler(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<CommandStatus> Handle(AddCurrencyCommand request, CancellationToken cancellationToken)
        {
            if (await context.currencies.AnyAsync(x => x.Name == request.Name, default))
                return CommandStatus.Failed("Exists currency with this name");

            if (await context.currencies.AnyAsync(x => x.CurrencyCode == request.CurrencyCode, default))
                return CommandStatus.Failed("Exists currency with this code");

            var currency = new Currency
            {
                Name = request.Name,
                CurrencyCode = request.CurrencyCode,
                ChangeRate = request.ChangeRate
            };

            await context.currencies.AddAsync(currency, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return new CommandStatus();
        }
    }
}
