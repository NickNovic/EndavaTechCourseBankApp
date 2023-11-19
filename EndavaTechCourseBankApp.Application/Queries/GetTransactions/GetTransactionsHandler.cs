using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EndavaTechCourseBankApp.Domain.Models;
using Microsoft.AspNetCore.Http;
using EndavaTechCourseBankApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EndavaTechCourseBankApp.Application.Queries.GetTransactions
{
    public class GetTransactionsHandler : IRequestHandler<GetTransactionsQuery, List<Transaction>>
    {
        private readonly ApplicationDbContext context;
        public GetTransactionsHandler(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Transaction>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            var transactions = new List<Transaction>();
            
            var wals = context.wallets.Where(w => w.UserId == request.UserId).AsNoTracking().ToList();
            

            foreach (var w in wals)
            {
                var transactionsforwallet = await context.transactions.Where(t => t.IdOfSender == w.Id || t.IdOfAccepter == w.Id).ToListAsync();
                transactions.AddRange(transactionsforwallet);
            }
            
            return transactions;
        }
    }
}
