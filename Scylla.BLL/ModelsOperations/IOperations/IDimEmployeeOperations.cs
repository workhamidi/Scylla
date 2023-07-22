using Scylla.BLL.Decorator;
using Scylla.BLL.Dtos.Models;

namespace Scylla.BLL.ModelsOperations.IOperations
{
    public interface IDimEmployeeOperations
    {
        public Task<ResponseWrapper<int>> CreateRecordAsync(DimEmployeeDto dimEmployeeDto);

        public Task<ResponseWrapper<DimEmployeeDto>> GetRecordsByIdAsync(int employeeKey);

        public Task<ResponseWrapper<List<DimEmployeeDto>>> GetAllRecordsAsync();

        public Task<ResponseWrapper<List<DimEmployeeDto>>> AdvanceSearch(string stringQuery);
        
        public Task<ResponseWrapper<DimEmployeeDto>> UpdateRecordAsync(DimEmployeeDto dimEmployeeDto);

        public Task<ResponseWrapper<int>> DeleteRecordAsync(int employeeKey);
    }
}
