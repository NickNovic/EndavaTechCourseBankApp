using EndavaTechCourseBankApp.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndavaTechCourseBankApp.Application.Commands.UpdateWallet
{
    public class UpdateWalletCommand : IRequest<CommandStatus>
    {
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public int Pincode { get; set; }
        public string Type { get; set; }
        public DateTime LastActivity { get; set; }
        public Guid CurrencyId { get; set; }
        public Guid WalletId { get; set; }
    }
}
