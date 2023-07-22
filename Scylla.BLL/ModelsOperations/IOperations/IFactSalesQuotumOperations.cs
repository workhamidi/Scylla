using Scylla.BLL.Dtos.Models;
using Scylla.BLL.Decorator;

namespace Scylla.BLL.ModelsOperations.IOperations
{
    public interface IFactSalesQuotumOperations
    {
        public Task<ResponseWrapper<int>> CreateRecordAsync(FactSalesQuotumDto factSalesQuotumDto);

        public Task<ResponseWrapper<FactSalesQuotumDto>> GetRecordsByIdAsync(int salesQuotaKey);

        public Task<ResponseWrapper<List<FactSalesQuotumDto>>> GetAllRecordsAsync();

        public Task<ResponseWrapper<List<FactSalesQuotumDto>>> AdvanceSearch(string stringQuery);

        public Task<ResponseWrapper<FactSalesQuotumDto>> UpdateRecordAsync(FactSalesQuotumDto factSalesQuotumDto);

        public Task<ResponseWrapper<int>> DeleteRecordAsync(int salesQuotaKey);
    }
}
