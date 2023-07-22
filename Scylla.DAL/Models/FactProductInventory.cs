﻿using System;
using System.Collections.Generic;

namespace Scylla.DAL.Models
{
    public partial class FactProductInventory
    {
        public int ProductKey { get; set; }
        public int DateKey { get; set; }
        public DateTime MovementDate { get; set; }
        public decimal UnitCost { get; set; }
        public int UnitsIn { get; set; }
        public int UnitsOut { get; set; }
        public int UnitsBalance { get; set; }

        public virtual DimDate DateKeyNavigation { get; set; } = null!;
        public virtual DimProduct ProductKeyNavigation { get; set; } = null!;
    }
}
