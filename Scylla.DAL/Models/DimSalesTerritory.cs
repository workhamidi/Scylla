using System;
using System.Collections.Generic;

namespace Scylla.DAL.Models
{
    public partial class DimSalesTerritory
    {
        public DimSalesTerritory()
        {
            DimEmployees = new HashSet<DimEmployee>();
            DimGeographies = new HashSet<DimGeography>();
            FactInternetSales = new HashSet<FactInternetSale>();
            FactResellerSales = new HashSet<FactResellerSale>();
        }

        public int SalesTerritoryKey { get; set; }
        public int? SalesTerritoryAlternateKey { get; set; }
        public string SalesTerritoryRegion { get; set; } = null!;
        public string SalesTerritoryCountry { get; set; } = null!;
        public string? SalesTerritoryGroup { get; set; }
        public byte[]? SalesTerritoryImage { get; set; }

        public virtual ICollection<DimEmployee> DimEmployees { get; set; }
        public virtual ICollection<DimGeography> DimGeographies { get; set; }
        public virtual ICollection<FactInternetSale> FactInternetSales { get; set; }
        public virtual ICollection<FactResellerSale> FactResellerSales { get; set; }
    }
}
