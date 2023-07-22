using Serilog;
using Serilog.Context;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Scylla.BLL.Dtos.Models;
using Scylla.BLL.Enums;
using Scylla.BLL.ModelsOperations.IOperations;


namespace Scylla.Ui.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DimEmployeeController : ControllerBase
    {
        private readonly IDimEmployeeOperations _dimEmployeeOperations;

        public DimEmployeeController(IDimEmployeeOperations dimEmployeeOperations)
        {
            _dimEmployeeOperations = dimEmployeeOperations;
        }

        // Post: api/DimEmployee/create
        [HttpPost("create")]
        public async Task<IActionResult> CreateDimEmployee(
            [FromBody] DimEmployeeDto dimEmployeeDto)
        {
           
            Log.Information(
                "Ui layer > CreateDimEmployee func - request to Create a new DimEmployee with the following informations :" +
                "{@0}", dimEmployeeDto);

            var responseCreateDimEmployee = await _dimEmployeeOperations
                .CreateRecordAsync(dimEmployeeDto);

            var jsonResponseCreateDimEmployee =
                JsonSerializer.Serialize(responseCreateDimEmployee);

            if (responseCreateDimEmployee.StatusCodeEnum
                == ResponseStatusCodes.DataBaseError)
            {
                return StatusCode(500, jsonResponseCreateDimEmployee);
            }
            else if (responseCreateDimEmployee.StatusCodeEnum
                      == ResponseStatusCodes.ExistRecord)
            {
                return BadRequest(jsonResponseCreateDimEmployee);
            }

            return Ok(jsonResponseCreateDimEmployee);
        }


        // GET: api/DimEmployee?...
        [HttpGet]
        public async Task<IActionResult> GetDimEmployeeById([FromQuery] int employeeKey)
        {
            var responseGetDimEmployeeById = await _dimEmployeeOperations
                         .GetRecordsByIdAsync(employeeKey);

            var jsonResponseGetDimEmployeeById =
                JsonSerializer.Serialize(responseGetDimEmployeeById);


            if (responseGetDimEmployeeById.StatusCodeEnum
                == ResponseStatusCodes.DataBaseError)
            {
                return StatusCode(500, jsonResponseGetDimEmployeeById);
            }
            else if (responseGetDimEmployeeById.StatusCodeEnum
                == ResponseStatusCodes.RecordNotFound)
            {
                return NotFound(jsonResponseGetDimEmployeeById);
            }


            return Ok(jsonResponseGetDimEmployeeById);

        }


        // GET: api/DimEmployee/
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllDimEmployee()
        {
            var responseGetAllDimEmployee =
                await _dimEmployeeOperations.GetAllRecordsAsync();

            var jsonResponseGetAllDimEmployee =
                JsonSerializer.Serialize(responseGetAllDimEmployee);

            if (responseGetAllDimEmployee.StatusCodeEnum
                == ResponseStatusCodes.DataBaseError)
            {
                return StatusCode(500, jsonResponseGetAllDimEmployee);
            }

            return Ok(JsonSerializer.Serialize(responseGetAllDimEmployee));
        }


        // GET: api/DimEmployee/AdvanceSearch?...
        [HttpGet("AdvanceSearch")]
        public async Task<IActionResult> DimEmployeeAdvanceSearch(
            [FromBody] AdvancedSearchDto advanceSearchQuery
        )
        {

            var responseDimEmployeeAdvanceSearch =
                await _dimEmployeeOperations.AdvanceSearch(
                    advanceSearchQuery.SearchStringQuery);

            var jsonResponseDimEmployeeAdvanceSearch =
                JsonSerializer.Serialize(responseDimEmployeeAdvanceSearch);


            if (responseDimEmployeeAdvanceSearch.StatusCodeEnum
                == ResponseStatusCodes.DataBaseError)
            {
                return StatusCode(500, jsonResponseDimEmployeeAdvanceSearch);
            }
            else if (responseDimEmployeeAdvanceSearch.StatusCodeEnum
                     == ResponseStatusCodes.RecordNotFound)
            {
                return NotFound(jsonResponseDimEmployeeAdvanceSearch);
            }

            return Ok(jsonResponseDimEmployeeAdvanceSearch);

        }


        // Put api/DimEmployee/update?...
        [HttpPut("update")]
        public async Task<IActionResult> PutUpdateDimEmployee(
            [FromBody] DimEmployeeDto dimEmployeeDto
            )
        {
            var responsePutUpdateDimEmployee = await _dimEmployeeOperations
                .UpdateRecordAsync(dimEmployeeDto);

            var jsonResponsePutUpdateDimEmployee =
                JsonSerializer.Serialize(responsePutUpdateDimEmployee);

            if (responsePutUpdateDimEmployee.StatusCodeEnum
                == ResponseStatusCodes.DataBaseError)
            {
                return StatusCode(500, jsonResponsePutUpdateDimEmployee);
            }
            else if (responsePutUpdateDimEmployee.StatusCodeEnum
                     == ResponseStatusCodes.RecordNotFound)
            {
                return NotFound(jsonResponsePutUpdateDimEmployee);
            }

            return Ok(jsonResponsePutUpdateDimEmployee);

        }


        // DELETE api/DimEmployee/delete/5
        [HttpDelete("delete/{dimEmployeeId}")]
        public async Task<IActionResult> DeleteDimEmployee(int dimEmployeeId)
        {

            var responseDeleteDimEmployee =
                await _dimEmployeeOperations.DeleteRecordAsync(dimEmployeeId);

            var jsonResponseDeleteDimEmployee =
                JsonSerializer.Serialize(responseDeleteDimEmployee);

            if (responseDeleteDimEmployee.StatusCodeEnum
                == ResponseStatusCodes.DataBaseError)
            {
                return StatusCode(500, jsonResponseDeleteDimEmployee);
            }
            else if (responseDeleteDimEmployee.StatusCodeEnum
                     == ResponseStatusCodes.RecordNotFound)
            {
                return NotFound(jsonResponseDeleteDimEmployee);
            }

            return Ok(jsonResponseDeleteDimEmployee);

        }
    }
}


// return StatusCode(500, "oopes"); 500
// return NotFound(); 404
// return NoContent(); 204 هیچی نشون نمی ده
// return BadRequest(); 400


/*
int EmployeeKey 
string FirstName 
string? LastName 
DateTime? BirthDate 
string? EmailAddress 
string? Phone 
*/