using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndavaTechCourseBankApp.Application.Commands.ChangeWalletTypePercent
{
    public class ChangeWalletTypePercentCommand : IRequest<CommandStatus>
    {
        public string TypeName;
        public float Percent;
    }
}
