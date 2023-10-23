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
        public Currency Currency { get; set; }
        public int Pincode { get; set; }
        public string Type { get; set; }
        public DateTime LastActivity { get; set; }
        public decimal ChangeRate { get; set; }
    }
}
