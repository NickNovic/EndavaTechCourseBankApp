using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndavaTechCourseBankApp.Application.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<CommandStatus>
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid MainWalletId { get; set; }
        public string Email { get; set; }
    }
}
