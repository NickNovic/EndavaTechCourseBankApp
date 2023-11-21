using EndavaTechCourseBankApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndavaTechCourseBankApp.Shared
{
    public class GetTransactionDTO
    {
        public Guid Id { get; set; }
        public Guid CurrencyId { get; set; }
        public decimal Amount { get; set; }
        public decimal ChangeRate { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string CodeOfAccepter { get; set; }
        public string CodeOfSender { get;set; }
    }
}
