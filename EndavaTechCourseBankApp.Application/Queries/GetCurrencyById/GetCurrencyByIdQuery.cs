using EndavaTechCourseBankApp.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndavaTechCourseBankApp.Application.Queries.GetCurrencyById
{
    public class GetCurrencyByIdQuery : IRequest<Currency>
    {
        public Guid Id { get; set; }
    }
}
