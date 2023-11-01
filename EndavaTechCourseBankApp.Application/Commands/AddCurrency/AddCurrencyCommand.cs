using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EndavaTechCourseBankApp.Application.Commands.AddCurrency
{
    public class AddCurrencyCommand : IRequest<CommandStatus>
    {
        public string Name { get; set; }
        public string CurrencyCode {  get; set; }   
        public decimal ChangeRate { get; set; }
    }
}
