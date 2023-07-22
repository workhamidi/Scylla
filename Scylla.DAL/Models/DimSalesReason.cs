using System;
using System.Collections.Generic;

namespace Scylla.DAL.Models
{
    public partial class DimSalesReason
    {
        public DimSalesReason()
        {
            SalesOrders = new HashSet<FactInternetSale>();
        }

        public int SalesReasonKey { get; set; }
        public int SalesReasonAlternateKey { get; set; }
        public string SalesReasonName { get; set; } = null!;
        public string SalesReasonReasonType { get; set; } = null!;

        public virtual ICollection<FactInternetSale> SalesOrders { get; set; }
    }
}
