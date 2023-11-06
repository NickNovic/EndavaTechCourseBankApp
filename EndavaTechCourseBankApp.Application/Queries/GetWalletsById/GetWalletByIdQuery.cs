using EndavaTechCourseBankApp.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndavaTechCourseBankApp.Application.Queries.GetWalletsById
{
    public class GetWalletByIdQuery : IRequest<Wallet>
    {
        public Guid Id { get; set; }
    }
}
