using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndavaTechCourseBankApp.Application.Commands.RemoveCodeFromFavorites
{
    public class RemoveCodeFromFavoritesCommand : IRequest<CommandStatus>
    {
        public string UserId { get; set; }
        public string WalletCode { get; set; }
    }
}
