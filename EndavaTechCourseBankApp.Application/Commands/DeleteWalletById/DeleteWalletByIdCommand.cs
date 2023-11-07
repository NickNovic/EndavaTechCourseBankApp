using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndavaTechCourseBankApp.Application.Commands.DeleteWalletById
{
    public class DeleteWalletByIdCommand : IRequest<CommandStatus>
    {
        public Guid Id { get; set; }
    }
}
