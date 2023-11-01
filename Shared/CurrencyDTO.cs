using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndavaTechCourseBankApp.Shared
{
    public class CurrencyDTO
    {
        public string Id { get; set; }
        public bool CanBeRemoved { get; set; } = true;
        public string CurrencyCode { get; set; }
        public decimal ChangeRate { get; set; }
        public string Name { get; set; }
    }
}
