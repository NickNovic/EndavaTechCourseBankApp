using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndavaTechCourseBankApp.Application.Queries.GetFavorites
{
    public class GetFavoritesQuery : IRequest<List<string>>
    {
        public Guid UserId;
    }
}
