using EndavaTechCourseBankApp.Domain.Models;
using EndavaTechCourseBankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndavaTechCourseBankApp.Application.Queries.GetCurrencies
{
    public class GetCurrenciesHandler : IRequestHandler<GetCurrenciesQuery, List<Currency>>
    {
        private readonly ApplicationDbContext context;
        public GetCurrenciesHandler(ApplicationDbContext context) 
        {
            this.context = context;
        }

        public async Task<List<Currency>> Handle(GetCurrenciesQuery request, CancellationToken cancellationToken)
        {
            var currencyes = await context.currencies.AsNoTracking().ToListAsync(cancellationToken);
            
            return currencyes;
        }
    }
}
