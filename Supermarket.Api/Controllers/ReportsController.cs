using Microsoft.AspNetCore.Mvc;
using Supermarket.Api.Data;
using Supermarket.Api.Model;

namespace Supermarket.Api.Controllers
{
    [Route("api/Reports")]
    public class ReportsController : Controller
    {
        private readonly IReportsRepository _repository;
        public ReportsController(IReportsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("GetReportProductsSold")] //api/Reports/GetReportProductsSold
        public async Task<IActionResult> GetReportProductsSold()
        {
            try
            {
                return Ok(await _repository.GetReportProductsSold());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetReporTopProducts")] //api/Reports/GetReporTopProducts
        public async Task<IActionResult> GetReporTopProducts()
        {
            try
            {
                return Ok(await _repository.GetReporTopProducts());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}