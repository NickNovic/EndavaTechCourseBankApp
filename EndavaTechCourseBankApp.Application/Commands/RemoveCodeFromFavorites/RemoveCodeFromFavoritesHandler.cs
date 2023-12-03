using EndavaTechCourseBankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndavaTechCourseBankApp.Application.Commands.RemoveCodeFromFavorites
{
    public class RemoveCodeFromFavoritesHandler : IRequestHandler<RemoveCodeFromFavoritesCommand, CommandStatus>
    {
        private readonly ApplicationDbContext _context;
        public RemoveCodeFromFavoritesHandler(ApplicationDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context, nameof(context));
            _context = context;
        }

        public async Task<CommandStatus> Handle(RemoveCodeFromFavoritesCommand request, CancellationToken cancellationToken)
        {
            var requestUserId = Guid.Parse(request.UserId);
            var user = await _context.Users.Include(u => u.Favorites).FirstOrDefaultAsync(u => u.Id == requestUserId);

            if (user is null)
                return CommandStatus.Failed("No such user");

            var walletCode = user.Favorites.FirstOrDefault(f => f.WalletCode == request.WalletCode);
            user.Favorites.Remove(walletCode);
            _context.SaveChanges();

            return new();
        }
    }
}
