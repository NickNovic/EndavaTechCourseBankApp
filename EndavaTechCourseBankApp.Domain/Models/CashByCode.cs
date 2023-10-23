using EndavaTechCourseBankApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndavaTechCourseBankApp.Domain.Models
{
    class CashByCode : BaseEntity
    {
        public int Code {  get; set; }
        public decimal Amount { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Currency Currency { get; set; }
    }
}
