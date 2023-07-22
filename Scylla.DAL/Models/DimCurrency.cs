using System;
using System.Collections.Generic;

namespace Scylla.DAL.Models
{
    public partial class DimCurrency
    {
        public DimCurrency()
        {
            DimOrganizations = new HashSet<DimOrganization>();
            FactCurrencyRates = new HashSet<FactCurrencyRate>();
            FactInternetSales = new HashSet<FactInternetSale>();
            FactResellerSales = new HashSet<FactResellerSale>();
        }

        public int CurrencyKey { get; set; }
        public string CurrencyAlternateKey { get; set; } = null!;
        public string CurrencyName { get; set; } = null!;

        public virtual ICollection<DimOrganization> DimOrganizations { get; set; }
        public virtual ICollection<FactCurrencyRate> FactCurrencyRates { get; set; }
        public virtual ICollection<FactInternetSale> FactInternetSales { get; set; }
        public virtual ICollection<FactResellerSale> FactResellerSales { get; set; }
    }
}
