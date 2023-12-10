using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndavaTechCourseBankApp.Application.Commands.AddToFavorites
{
    public class AddToFavoritesCommand : IRequest<CommandStatus>
    {
        public string WalletCode;
        public Guid UserId;
    }
}
