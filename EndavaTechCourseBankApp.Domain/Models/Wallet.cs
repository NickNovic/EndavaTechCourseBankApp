using EndavaTechCourseBankApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndavaTechCourseBankApp.Domain.Models
{
    public class Wallet : BaseEntity
    {
        public decimal Amount { get; set; }
        public Guid CurrencyId { get; set; }
        public int Pincode { get; set; }
        public string Type { get; set; }
        public DateTime LastActivity { get; set; }
        public Guid UserId { get; set; }
        public string WalletCode { get; set; }
    }
}
