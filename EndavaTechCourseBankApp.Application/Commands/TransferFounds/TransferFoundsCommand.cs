using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndavaTechCourseBankApp.Application.Commands.TransferFounds
{
    public class TransferFoundsCommand : IRequest<CommandStatus>
    {
        public Guid CurrencyId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string CodeOfAccepter { get; set; }
        public string CodeOfSender { get; set; }
    }
}
