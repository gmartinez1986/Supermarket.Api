using Microsoft.AspNetCore.Mvc;
using Supermarket.Api.Data;
using Supermarket.Api.Model;

namespace Supermarket.Api.Controllers
{
    [Route("api/Products")]
    public class ProductsController : Controller
    {
        private readonly IProductsRepository _repository;
        public ProductsController(IProductsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("GetProducts")] //api/Products/GetProducts
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
        [Route("GetProduct")] //api/Products/GetProduct/id
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
        [Route("CreateProduct")] //api/Products/CreateProduct
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            try
            {
                decimal decimalValue;
                DateTime dateTimeValue;

                if (product == null)
                {
                   return BadRequest("El producto no puede ser nulo.");
                }

                if (!decimal.TryParse(product.Price.ToString(), out decimalValue))
                {
                    return BadRequest("El precio tiene que ser un valor de tipo decimal.");
                }

                if(decimalValue <= 0)
                {
                    return BadRequest("El precio debe ser mayor a cero.");
                }

                if (!DateTime.TryParse(product.DateOfExpirity.ToString(), out dateTimeValue))
                {
                    return BadRequest("La fecha de expiración debe ser del tipo DateTime.");
                }

                return Ok(await _repository.CreateProduct(product));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut] 
        [Route("EditProduct")] //api/Products/EditProduct/id
        public async Task<IActionResult> EditProduct([FromQuery] int id, [FromBody] Product product)
        {
            try
            {
                decimal decimalValue;
                DateTime dateTimeValue;

                if (product == null)
                {
                    return BadRequest("El producto no puede ser nulo.");
                }

                if (!decimal.TryParse(product.Price.ToString(), out decimalValue))
                {
                    return BadRequest("El precio tiene que ser un valor de tipo decimal.");
                }

                if (decimalValue <= 0)
                {
                    return BadRequest("El precio debe ser mayor a cero.");
                }

                if (!DateTime.TryParse(product.DateOfExpirity.ToString(), out dateTimeValue))
                {
                    return BadRequest("La fecha de expiración debe ser del tipo DateTime.");
                }

                product.Id = id;

                return Ok(await _repository.EditProduct(product));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteProduct")] //api/Products/DeleteProduct/id
        public async Task<IActionResult> DeleteProduct([FromQuery] int id)
        {
            try
            {
                return Ok(await _repository.DeleteProduct(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
