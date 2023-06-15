using Microsoft.AspNetCore.Mvc;
using Supermarket.Api.Data;
using System.Diagnostics;

namespace Supermarket.Api.Controllers
{
    [Route("api/supermarket")]
    public class SupermarketController : Controller
    {
        private readonly IRepository _repository;
        public SupermarketController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("GetProducts")] //api/supermarket/GetProducts
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                return Ok(await _repository.GetProducts());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetProduct")] //api/supermarket/GetProduct/id
        public async Task<IActionResult> GetProduct([FromQuery] int id)
        {
            try
            {
                return Ok(await _repository.GetProduct(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
