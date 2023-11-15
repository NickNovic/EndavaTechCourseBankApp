using EndavaTechCourseBankApp.Domain.Models;
using EndavaTechCourseBankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndavaTechCourseBankApp.Application.Queries.GetWalletsById
{
    public class GetWalletByIdHandler : IRequestHandler<GetWalletByIdQuery, Wallet>
    {
        private readonly ApplicationDbContext context;
        public GetWalletByIdHandler(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Wallet> Handle(GetWalletByIdQuery request, CancellationToken cancellationToken)
        {
            var wallet = await context.wallets.FirstOrDefaultAsync(w => w.Id == request.Id);
            return wallet;
        }
    }
}
