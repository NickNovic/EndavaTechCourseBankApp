using EndavaTechCourseBankApp.Domain.Enums;
using EndavaTechCourseBankApp.Domain.Models;
using EndavaTechCourseBankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndavaTechCourseBankApp.Application.Queries.GetWalletTypes
{
    public class GetWalletTypesHandler : IRequestHandler<GetWalletTypesQuery, List<Commision>>
    {
        private readonly ApplicationDbContext _context;
        public GetWalletTypesHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Commision>> Handle(GetWalletTypesQuery request, CancellationToken cancellationToken)
        {
            var types = _context.commisions.ToList();

            return types;
        }
    }
}
