using Scylla.BLL.Dtos.Models;
using Scylla.DAL.Models;

namespace Scylla.BLL.Dtos.Mappers
{
    public static class DimEmployeeMapper
    {
        public static DimEmployee DtoToEntity(this DimEmployeeDto dto)
        {
            return new DimEmployee()
            {
                EmployeeKey = dto.EmployeeKey,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate,
                EmailAddress = dto.EmailAddress,
                Phone = dto.Phone,
                FactSalesQuota =
                    dto.FactSalesQuotum is null ? new List<FactSalesQuotum>() :
                        dto.FactSalesQuotum
                            .Select(x => x.DtoToEntity()).ToList()
            }; ;
        }

        public static DimEmployeeDto EntityToDto(this DimEmployee entity)
        {
            return new DimEmployeeDto()
            {
                EmployeeKey = entity.EmployeeKey,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                BirthDate = entity.BirthDate,
                EmailAddress = entity.EmailAddress,
                Phone = entity.Phone,
                FactSalesQuotum = entity.FactSalesQuota
                    .Select(x => x.EntityToDto()).ToList()
            };
        }

    }
}
