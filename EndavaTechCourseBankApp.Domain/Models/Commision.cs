using EndavaTechCourseBankApp.Domain.Common;
using EndavaTechCourseBankApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndavaTechCourseBankApp.Domain.Models
{
    public class Commision : BaseEntity
    {
        public WalletType Type { get; set; }
        public float Percent {  get; set; }
    }
}
