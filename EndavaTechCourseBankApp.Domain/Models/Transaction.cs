using EndavaTechCourseBankApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndavaTechCourseBankApp.Domain.Models
{
    class Transaction : BaseEntity
    {
        public Currency Currency { get; set; }
        public decimal Amount {  get; set; }
        public decimal Commision { get; set; }
        public decimal ChangeRate { get; set; }
        public string Description { get; set; }
    }
}
