using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scylla.BLL.Dtos.Models
{
    public class FactSalesQuotumDto
    {
        public int SalesQuotaKey { get; set; }

      
        public int EmployeeKey { get; set; }
        
        public int DateKey { get; set; }
                
        public short CalendarYear { get; set; }

        public byte CalendarQuarter { get; set; } 
        
        public decimal SalesAmountQuota { get; set; }
       
        public DateTime? Date { get; set; }
        
    }
}
