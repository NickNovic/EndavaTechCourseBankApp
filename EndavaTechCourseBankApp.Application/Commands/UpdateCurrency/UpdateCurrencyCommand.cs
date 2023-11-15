using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndavaTechCourseBankApp.Application.Commands.UpdateCurrency
{
    public class UpdateCurrencyCommand : IRequest<CommandStatus>
    {
        public string Name {  get; set; }
        public decimal ChangeRate {  get; set; }
        public string CurrencyCode {  get; set; }
        public Guid CurrencyId { get; set; }
    }
}
