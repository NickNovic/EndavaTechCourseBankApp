﻿using EndavaTechCourseBankApp.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndavaTechCourseBankApp.Application.Queries.GetTransactions
{
    public class GetTransactionsQuery : IRequest<List<Transaction>>
    {
        public Guid UserId { get; set; }
    }
}
