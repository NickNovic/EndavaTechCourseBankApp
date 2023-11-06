using EndavaTechCourseBankApp.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndavaTechCourseBankApp.Application.Commands.CreateWallet
{
    public class CreateWalletCommand : IRequest<CommandStatus>
    {
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public string Type { get; set; }    }
}
