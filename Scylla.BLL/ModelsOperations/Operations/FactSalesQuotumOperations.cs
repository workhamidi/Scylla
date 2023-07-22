using Gridify;
using Microsoft.EntityFrameworkCore;
using Scylla.BLL.Decorator;
using Scylla.BLL.Dtos.Mappers;
using Scylla.BLL.Dtos.Models;
using Scylla.BLL.Enums;
using Scylla.BLL.ModelsOperations.IOperations;
using Scylla.DAL.Context;
using Scylla.DAL.Models;


namespace Scylla.BLL.ModelsOperations.Operations
{
    public class FactSalesQuotumOperations : IFactSalesQuotumOperations
    {
        private readonly AdventureWorksDW2019Context _context;

        public FactSalesQuotumOperations(AdventureWorksDW2019Context context)
        {
            _context = context;
        }

        public async Task<ResponseWrapper<int>> CreateRecordAsync(FactSalesQuotumDto factSalesQuotumDto)
        {
            var wrapper = new ResponseWrapper<int>();

            try
            {
                var getFactSales = await _context.FactSalesQuota.FindAsync(factSalesQuotumDto.SalesQuotaKey);

                // if exist FactSalesQuota return it
                if (getFactSales is not null)
                {
                    wrapper.StatusCodeEnum = ResponseStatusCodes.ExistRecord;
                    wrapper.Description = "the Record exist.";
                    return wrapper;
                }

                var newFactSales = await _context.FactSalesQuota.AddAsync(factSalesQuotumDto.DtoToEntity());

                await _context.SaveChangesAsync();

                wrapper.StatusCodeEnum = ResponseStatusCodes.Success;
                wrapper.Description = "Successfully created record. new record id is : " +
                                      newFactSales.Entity.EntityToDto().SalesQuotaKey;
                wrapper.Content = newFactSales.Entity.EntityToDto().SalesQuotaKey;
            }
            catch (Exception e)
            {
                wrapper.StatusCodeEnum = ResponseStatusCodes.DataBaseError;
                wrapper.Description = e.Message;
            }

            return wrapper;
        }

        public async Task<ResponseWrapper<FactSalesQuotumDto>> GetRecordsByIdAsync(int salesQuotaKey)
        {
            var wrapper = new ResponseWrapper<FactSalesQuotumDto>();

            try
            {
                var getFactSalesById = await _context.FactSalesQuota.FindAsync(salesQuotaKey);

                if (getFactSalesById is null)
                {
                    wrapper.StatusCodeEnum = ResponseStatusCodes.RecordNotFound;
                    wrapper.Description = "the record was not found";
                    return wrapper;
                }

                wrapper.StatusCodeEnum = ResponseStatusCodes.Success;
                wrapper.Content = getFactSalesById!.EntityToDto();
            }
            catch (Exception e)
            {
                wrapper.StatusCodeEnum = ResponseStatusCodes.DataBaseError;
                wrapper.Description = e.Message;
            }
            return wrapper;
        }

        public async Task<ResponseWrapper<List<FactSalesQuotumDto>>> GetAllRecordsAsync()
        {
            var wrapper = new ResponseWrapper<List<FactSalesQuotumDto>>();
            try
            {
                var getAllFactSales = await _context.FactSalesQuota
                    .Select(x => x.EntityToDto())
                    .ToListAsync();

                wrapper.StatusCodeEnum = ResponseStatusCodes.Success;
                wrapper.Content = getAllFactSales;

                return wrapper;
            }
            catch (Exception e)
            {
                wrapper.StatusCodeEnum = ResponseStatusCodes.DataBaseError;
                wrapper.Description = e.Message;
            }

            return wrapper;
        }


        public async Task<ResponseWrapper<List<FactSalesQuotumDto>>> AdvanceSearch(string stringQuery)
        {
            var wrapper = new ResponseWrapper<List<FactSalesQuotumDto>>();

            try
            {

                var sactSalesQuotumAdvanceSearch = await _context.FactSalesQuota
                    // Gridify method
                    .ApplyFiltering(stringQuery)
                    .Select(i => i.EntityToDto())
                    .ToListAsync();

                if (sactSalesQuotumAdvanceSearch.Count == 0)
                {
                    wrapper.StatusCodeEnum = ResponseStatusCodes.RecordNotFound;
                    wrapper.Description = "the record/s was not found";
                    return wrapper;
                }

                wrapper.StatusCodeEnum = ResponseStatusCodes.Success;
                wrapper.Content = sactSalesQuotumAdvanceSearch;

                return wrapper;
            }
            catch (Exception e)
            {
                wrapper.StatusCodeEnum = ResponseStatusCodes.DataBaseError;
                wrapper.Description = e.Message;
            }

            return wrapper;

        }

        public async Task<ResponseWrapper<FactSalesQuotumDto>> UpdateRecordAsync(FactSalesQuotumDto factSalesQuotumDto)
        {
            var wrapper = new ResponseWrapper<FactSalesQuotumDto>();

            try
            {
                var getFactSales = _context.FactSalesQuota
                    .AsNoTracking()
                    .Where(i => i.SalesQuotaKey == factSalesQuotumDto.SalesQuotaKey)
                    .ToList()
                    .FirstOrDefault();

                if (getFactSales is null)
                {
                    wrapper.StatusCodeEnum = ResponseStatusCodes.RecordNotFound;
                    return wrapper;
                }

                getFactSales = factSalesQuotumDto.DtoToEntity();

                var updateFactSales = _context.FactSalesQuota
                    .Update(getFactSales);

                await _context.SaveChangesAsync();

                wrapper.StatusCodeEnum = ResponseStatusCodes.Success;
                wrapper.Content = updateFactSales.Entity.EntityToDto();
            }
            catch (Exception e)
            {
                wrapper.StatusCodeEnum = ResponseStatusCodes.DataBaseError;
                wrapper.Description = e.Message;
            }
            return wrapper;
        }

        public async Task<ResponseWrapper<int>> DeleteRecordAsync(int salesQuotaKey)
        {
            var wrapper = new ResponseWrapper<int>();

            try
            {
                var getFactSales = _context.FactSalesQuota
                    .AsNoTracking()
                    .Where(i => i.SalesQuotaKey == salesQuotaKey)
                    .ToList()
                    .FirstOrDefault();

                if (getFactSales is null)
                {
                    wrapper.StatusCodeEnum = ResponseStatusCodes.RecordNotFound;
                    return wrapper;
                }

                _context.FactSalesQuota.Remove(getFactSales);
                await _context.SaveChangesAsync();

                wrapper.StatusCodeEnum = ResponseStatusCodes.Success;

            }
            catch (Exception e)
            {
                wrapper.StatusCodeEnum = ResponseStatusCodes.DataBaseError;
                wrapper.Description = e.Message;
            }

            
            return wrapper;

        }

      
    }
}
// SalesQuotaKey  => identity column ما نمی تونیم مقدار بدی
// EmployeeKey = employeeKey,
// DateKey = dateKey,
// CalendarYear = calendarYear,
// CalendarQuarter = calendarQuarter,
// SalesAmountQuota = salesAmountQuota,
// Date = date
//  ما با اینا کاری نداریم برای خود ef‌ هست 
// DateKeyNavigation
// EmployeeKeyNavigation