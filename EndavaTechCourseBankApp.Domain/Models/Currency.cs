﻿using EndavaTechCourseBankApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndavaTechCourseBankApp.Domain.Models
{
    public class Currency : BaseEntity
    {
        public string CurrencyCode { get; set; }
        public decimal PriceSell { get; set; }
        public decimal PriceBuy {  get; set; } 
        public string Name { get; set; }
    }
}
