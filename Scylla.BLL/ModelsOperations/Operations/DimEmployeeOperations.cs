using Gridify;
using Serilog;
using Microsoft.EntityFrameworkCore;
using Scylla.BLL.Decorator;
using Scylla.BLL.Dtos.Mappers;
using Scylla.BLL.Dtos.Models;
using Scylla.BLL.Enums;
using Scylla.BLL.ModelsOperations.IOperations;
using Scylla.DAL.Context;

namespace Scylla.BLL.ModelsOperations.Operations
{

    public class DimEmployeeOperations : IDimEmployeeOperations
    {
        private readonly AdventureWorksDW2019Context _context;

        public DimEmployeeOperations(AdventureWorksDW2019Context context)
        {
            _context = context;
        }

        public async Task<ResponseWrapper<int>> CreateRecordAsync(
            DimEmployeeDto dimEmployeeDto
            )
        {
            
            var wrapper = new ResponseWrapper<int>();

            try
            {
                Log.Information(
                    "Bll layer >  CreateRecordAsync func : request to Create a new DimEmployee");

                var getDimEmp = _context.DimEmployees
                    .FirstOrDefault(i =>
                        i.LastName == dimEmployeeDto.LastName &&
                        i.FirstName == dimEmployeeDto.FirstName);

                // if exist DimEmployee return it
                if (getDimEmp is not null)
                {
                    Log.Warning(
                        "Bll layer >  CreateRecordAsync func : DimEmployee existed");
                    wrapper.StatusCodeEnum = ResponseStatusCodes.ExistRecord;
                    wrapper.Description = "the Firstname and Lastname already exist in the database+";
                    return wrapper;
                }

                var newDimEmployee = await _context
                    .DimEmployees.AddAsync(dimEmployeeDto.DtoToEntity());

                await _context.SaveChangesAsync();

                wrapper.StatusCodeEnum = ResponseStatusCodes.Success;
                wrapper.Description = "Successfully created record. new record id is s : " +
                                      newDimEmployee.Entity.EntityToDto().EmployeeKey;
                wrapper.Content = newDimEmployee.Entity.EntityToDto().EmployeeKey;

                return wrapper;

            }
            catch (Exception e)
            {
                Log.Error(
                    "Bll layer >  CreateRecordAsync func : database error : {0}",e.Message);
                wrapper.StatusCodeEnum = ResponseStatusCodes.DataBaseError;
                wrapper.Description = e.Message;
                return wrapper;
            }
        }

        public async Task<ResponseWrapper<DimEmployeeDto>> GetRecordsByIdAsync(
            int employeeKey)
        {

            var wrapper = new ResponseWrapper<DimEmployeeDto>();


            try
            {
                var getDimEmpById = await _context.DimEmployees.FindAsync(employeeKey);

                if (getDimEmpById is null)
                {
                    wrapper.StatusCodeEnum = ResponseStatusCodes.RecordNotFound;
                    wrapper.Description = "the record was not found";
                    return wrapper;
                }

                wrapper.StatusCodeEnum = ResponseStatusCodes.Success;
                wrapper.Content = getDimEmpById!.EntityToDto();
            }
            catch (Exception e)
            {
                wrapper.StatusCodeEnum = ResponseStatusCodes.DataBaseError;
                wrapper.Description = e.Message;
                return wrapper;
            }


            return wrapper;

        }

        public async Task<ResponseWrapper<List<DimEmployeeDto>>> GetAllRecordsAsync()
        {
            var wrapper = new ResponseWrapper<List<DimEmployeeDto>>();

            try
            {
                var getAllDimEmp = await _context.DimEmployees
                    .Select(x => x.EntityToDto())
                    .ToListAsync();

                wrapper.StatusCodeEnum = ResponseStatusCodes.Success;
                wrapper.Content = getAllDimEmp;

                return wrapper;
            }
            catch (Exception e)
            {
                wrapper.StatusCodeEnum = ResponseStatusCodes.DataBaseError;
                wrapper.Description = e.Message;
            }

            return wrapper;
        }

        public async Task<ResponseWrapper<List<DimEmployeeDto>>> AdvanceSearch(string stringQuery)
        {
            var wrapper = new ResponseWrapper<List<DimEmployeeDto>>();

            try
            {

                var dimEmployeesAdvanceSearch = await _context.DimEmployees
                    // Gridify method
                    .ApplyFiltering(stringQuery)
                    .Select(i => i.EntityToDto())
                    .ToListAsync();

                if (dimEmployeesAdvanceSearch.Count == 0)
                {
                    wrapper.StatusCodeEnum = ResponseStatusCodes.RecordNotFound;
                    wrapper.Description = "the record/s was not found";
                    return wrapper;
                }

                wrapper.StatusCodeEnum = ResponseStatusCodes.Success;
                wrapper.Content = dimEmployeesAdvanceSearch;

                return wrapper;
            }
            catch (Exception e)
            {
                wrapper.StatusCodeEnum = ResponseStatusCodes.DataBaseError;
                wrapper.Description = e.Message;
            }

            return wrapper;

        }

        public async Task<ResponseWrapper<DimEmployeeDto>> UpdateRecordAsync(
            DimEmployeeDto dimEmployeeDto)
        {
            var wrapper = new ResponseWrapper<DimEmployeeDto>();
            try
            {
                var getDimEmp = _context.DimEmployees
                    .AsNoTracking()
                    .Where(i => i.EmployeeKey == dimEmployeeDto.EmployeeKey)
                    .ToList()
                    .FirstOrDefault();

                if (getDimEmp is null)
                {
                    wrapper.StatusCodeEnum = ResponseStatusCodes.RecordNotFound;
                    return wrapper;
                }

                getDimEmp = dimEmployeeDto.DtoToEntity();

                var updateDimEmployee = _context.DimEmployees
                    .Update(getDimEmp);

                await _context.SaveChangesAsync();

                wrapper.StatusCodeEnum = ResponseStatusCodes.Success;
                wrapper.Content = updateDimEmployee.Entity.EntityToDto();
            }
            catch (Exception e)
            {
                wrapper.StatusCodeEnum = ResponseStatusCodes.DataBaseError;
                wrapper.Description = e.Message;
            }

            return wrapper;
        }

        public async Task<ResponseWrapper<int>> DeleteRecordAsync(int dimEmployeeId)
        {
            var wrapper = new ResponseWrapper<int>();

            try
            {
                var getDimEmp = _context.DimEmployees
                    .AsNoTracking()
                    .Where(i => i.EmployeeKey == dimEmployeeId)
                    .ToList()
                    .FirstOrDefault();


                if (getDimEmp is null)
                {
                    wrapper.StatusCodeEnum = ResponseStatusCodes.RecordNotFound;
                    return wrapper;
                }

                _context.DimEmployees.Remove(getDimEmp);
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
