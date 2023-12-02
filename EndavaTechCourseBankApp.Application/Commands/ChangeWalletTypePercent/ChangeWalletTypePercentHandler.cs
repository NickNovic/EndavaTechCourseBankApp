using EndavaTechCourseBankApp.Domain.Enums;
using EndavaTechCourseBankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndavaTechCourseBankApp.Application.Commands.ChangeWalletTypePercent
{
    public class ChangeWalletTypePercentHandler : IRequestHandler<ChangeWalletTypePercentCommand, CommandStatus>
    {
        private readonly ApplicationDbContext _context;

        public ChangeWalletTypePercentHandler(ApplicationDbContext context)
        {

            ArgumentNullException.ThrowIfNull(context);

            _context = context;
        }

        public async Task<CommandStatus> Handle(ChangeWalletTypePercentCommand request, CancellationToken cancellationToken)
        {
            if (request.TypeName is null || request.Percent == 0)
                return CommandStatus.Failed("no parameters");

            WalletType type = (WalletType)Enum.Parse(typeof(WalletType), request.TypeName);
            var commision = await _context.commisions.FirstOrDefaultAsync(c => c.Type == type);
            if (commision is null)
                return CommandStatus.Failed("no such type");

            commision.Percent = request.Percent;

            _context.SaveChanges();

            return new();
        }
    }
}
