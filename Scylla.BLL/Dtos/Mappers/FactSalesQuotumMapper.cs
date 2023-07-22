
using Scylla.BLL.Dtos.Models;
using Scylla.DAL.Models;

namespace Scylla.BLL.Dtos.Mappers
{
    public static class FactSalesQuotumMapper
    {
        public static FactSalesQuotum DtoToEntity(this FactSalesQuotumDto dto)
        {
            return new FactSalesQuotum()
            {

                SalesQuotaKey = dto.SalesQuotaKey,
                EmployeeKey = dto.EmployeeKey,
                DateKey = dto.DateKey,
                CalendarYear = dto.CalendarYear,
                CalendarQuarter = dto.CalendarQuarter,
                SalesAmountQuota = dto.SalesAmountQuota,
                Date = dto.Date
            };
        }

        public static FactSalesQuotumDto EntityToDto(this FactSalesQuotum entity)
        {
            return new FactSalesQuotumDto()
            {
                SalesQuotaKey = entity.SalesQuotaKey,
                EmployeeKey = entity.EmployeeKey,
                DateKey = entity.DateKey,
                CalendarYear = entity.CalendarYear,
                CalendarQuarter = entity.CalendarQuarter,
                SalesAmountQuota = entity.SalesAmountQuota,
                Date = entity.Date
            };
        }
    }
}
