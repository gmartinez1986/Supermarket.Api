using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Supermarket.Api.Data;
using Supermarket.Api.Model;
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

        [HttpPost]
        [Route("CreateProduct")] //api/supermarket/CreateProduct
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            try
            {
                if (product == null)
                {
                   return BadRequest("El producto no puede ser nulo.");
                }

                return Ok(await _repository.CreateProduct(product));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
