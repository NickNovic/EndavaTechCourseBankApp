using EndavaTechCourseBankApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndavaTechCourseBankApp.Domain.Models
{
    class Report : BaseEntity
    {
        public List<Transaction> Transactions { get; set; }
        public List<CashByCode > ActiceCashByCodes { get; set; }
        public List<Wallet> Wallets { get; set; }
    }
}
