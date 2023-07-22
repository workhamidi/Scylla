using System;
using System.Collections.Generic;

namespace Scylla.DAL.Models
{
    public partial class DimProductCategory
    {
        public DimProductCategory()
        {
            DimProductSubcategories = new HashSet<DimProductSubcategory>();
        }

        public int ProductCategoryKey { get; set; }
        public int? ProductCategoryAlternateKey { get; set; }
        public string EnglishProductCategoryName { get; set; } = null!;
        public string SpanishProductCategoryName { get; set; } = null!;
        public string FrenchProductCategoryName { get; set; } = null!;

        public virtual ICollection<DimProductSubcategory> DimProductSubcategories { get; set; }
    }
}
