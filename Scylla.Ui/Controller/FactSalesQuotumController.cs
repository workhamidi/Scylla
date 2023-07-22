using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Scylla.BLL.Dtos.Models;
using Scylla.BLL.Enums;
using Scylla.BLL.ModelsOperations.IOperations;


namespace Scylla.Ui.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FactSalesQuotumController : ControllerBase
    {
        private readonly IFactSalesQuotumOperations _factSalesQuotumOperations;

        public FactSalesQuotumController(IFactSalesQuotumOperations factSalesQuotumOperations)
        {
            _factSalesQuotumOperations = factSalesQuotumOperations;
        }

        // Post: api/FactSalesQuotum/create
        [HttpPost("create")]
        public async Task<IActionResult> CreateFactSales(
           [FromBody] FactSalesQuotumDto factSalesDto)
        {
            var responseCreateFactSales = await _factSalesQuotumOperations
                .CreateRecordAsync(factSalesDto);

            var jsonResponseCreateFactSales =
                JsonSerializer.Serialize(responseCreateFactSales);


            if (responseCreateFactSales.StatusCodeEnum
                == ResponseStatusCodes.DataBaseError)
            {
                return StatusCode(500, jsonResponseCreateFactSales);
            }
            else if (responseCreateFactSales.StatusCodeEnum
                     == ResponseStatusCodes.ExistRecord)
            {
                return BadRequest(jsonResponseCreateFactSales);
            }

            return Ok(jsonResponseCreateFactSales);

        }

        // GET: api/FactSalesQuotum?...
        [HttpGet]
        public async Task<IActionResult> GetFactSalesById([FromQuery] int salesQuotaKey)
        {
            var responseGetFactSalesById = await _factSalesQuotumOperations
                .GetRecordsByIdAsync(salesQuotaKey);

            var jsonResponseGetFactSalesById =
                JsonSerializer.Serialize(responseGetFactSalesById);

            if (responseGetFactSalesById.StatusCodeEnum
                == ResponseStatusCodes.DataBaseError)
            {
                return StatusCode(500, jsonResponseGetFactSalesById);
            }
            else if (responseGetFactSalesById.StatusCodeEnum
                     == ResponseStatusCodes.RecordNotFound)
            {
                return NotFound(responseGetFactSalesById);
            }

            return Ok(jsonResponseGetFactSalesById);

        }


        // GET: api/FactSalesQuotum/
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllFactSales()
        {
            var responseGetAllFactSales =
                await _factSalesQuotumOperations.GetAllRecordsAsync();

            var jsonResponseGetAllFactSales =
                JsonSerializer.Serialize(responseGetAllFactSales);

            if (responseGetAllFactSales.StatusCodeEnum
                == ResponseStatusCodes.DataBaseError)
            {
                return StatusCode(500, jsonResponseGetAllFactSales);
            }


            return Ok(jsonResponseGetAllFactSales);

        }


        // GET: api/DimEmployee/AdvanceSearch?...
        [HttpGet("AdvanceSearch")]
        public async Task<IActionResult> FactSalesAdvanceSearch(
            [FromBody] AdvancedSearchDto advanceSearchQuery
        )
        {

            var responseFactSalesAdvanceSearch =
                await _factSalesQuotumOperations.AdvanceSearch(
                    advanceSearchQuery.SearchStringQuery);

            var jsonResponseFactSalesAdvanceSearch =
                JsonSerializer.Serialize(responseFactSalesAdvanceSearch);


            if (responseFactSalesAdvanceSearch.StatusCodeEnum
                == ResponseStatusCodes.DataBaseError)
            {
                return StatusCode(500, jsonResponseFactSalesAdvanceSearch);
            }
            else if (responseFactSalesAdvanceSearch.StatusCodeEnum
                     == ResponseStatusCodes.RecordNotFound)
            {
                return NotFound(jsonResponseFactSalesAdvanceSearch);
            }

            return Ok(jsonResponseFactSalesAdvanceSearch);

        }



        // Put api/FactSalesQuotum/update?...
        [HttpPut("update")]
        public async Task<IActionResult> PutUpdateFactSales(
            [FromBody] FactSalesQuotumDto factSalesDto
            )
        {
            var responsePutUpdateFactSales = await _factSalesQuotumOperations
                .UpdateRecordAsync(factSalesDto);

            var jsonResponsePutUpdateFactSales =
                JsonSerializer.Serialize(responsePutUpdateFactSales);

            if (responsePutUpdateFactSales.StatusCodeEnum
                == ResponseStatusCodes.DataBaseError)
            {
                return StatusCode(500, jsonResponsePutUpdateFactSales);
            }
            else if (responsePutUpdateFactSales.StatusCodeEnum
                     == ResponseStatusCodes.RecordNotFound)
            {
                return NotFound(jsonResponsePutUpdateFactSales);
            }

            return Ok(jsonResponsePutUpdateFactSales);
        }


        // DELETE api/FactSalesQuotum/delete/5
        [HttpDelete("delete/{factSalesQuotumId}")]
        public async Task<IActionResult> DeleteFactSales(int factSalesQuotumId)
        {
            var responseDeleteFactSales = await _factSalesQuotumOperations
                .DeleteRecordAsync(factSalesQuotumId);

            var jsonResponseDeleteFactSales =
                JsonSerializer.Serialize(responseDeleteFactSales);

            if (responseDeleteFactSales.StatusCodeEnum
                == ResponseStatusCodes.DataBaseError)
            {
                return StatusCode(500, jsonResponseDeleteFactSales);
            }
            else if (responseDeleteFactSales.StatusCodeEnum
                     == ResponseStatusCodes.RecordNotFound)
            {
                return NotFound(jsonResponseDeleteFactSales);
            }

            return Ok(jsonResponseDeleteFactSales);
        }
    }
}















//اگر اول / رو بزاریم می ره از اول Url‌می خونه یعنی 
//[HttpGet("/a{id}")] => https://localhost:7221/a5
//ولی 
//[HttpGet("a{id}")] => https://localhost:7221/api/factSalesQuotum/a5

// Multiple Return Types :
// Object - فقط برای استفاده باید به نوع مورد نظرمون تبدیلش کنیم 
// dynamic
//
//  دو راه دیگه هم هست
// Record
// (,,,,) به جای مقدار بازگشتی
// https://stackoverflow.com/questions/34798681/method-with-multiple-return-types

