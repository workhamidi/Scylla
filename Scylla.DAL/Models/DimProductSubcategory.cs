using System;
using System.Collections.Generic;

namespace Scylla.DAL.Models
{
    public partial class DimProductSubcategory
    {
        public DimProductSubcategory()
        {
            DimProducts = new HashSet<DimProduct>();
        }

        public int ProductSubcategoryKey { get; set; }
        public int? ProductSubcategoryAlternateKey { get; set; }
        public string EnglishProductSubcategoryName { get; set; } = null!;
        public string SpanishProductSubcategoryName { get; set; } = null!;
        public string FrenchProductSubcategoryName { get; set; } = null!;
        public int? ProductCategoryKey { get; set; }

        public virtual DimProductCategory? ProductCategoryKeyNavigation { get; set; }
        public virtual ICollection<DimProduct> DimProducts { get; set; }
    }
}
