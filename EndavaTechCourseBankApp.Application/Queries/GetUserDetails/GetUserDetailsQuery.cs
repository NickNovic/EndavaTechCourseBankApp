using EndavaTechCourseBankApp.Domain.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndavaTechCourseBankApp.Application.Queries.GetUserDetails
{
    public class GetUserDetailsQuery : IRequest<UserDetails>
    {
        public string Username { get; set; }
    }
}
