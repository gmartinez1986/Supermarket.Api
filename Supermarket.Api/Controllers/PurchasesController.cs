using Microsoft.AspNetCore.Mvc;
using Supermarket.Api.Data;
using Supermarket.Api.Model;

namespace Supermarket.Api.Controllers
{
    [Route("api/Purchases")]
    public class PurchasesController : Controller
    {
        private readonly IPurchasesRepository _purchasesRepository;
        private readonly IProductsRepository _productsRepository;
        public PurchasesController(IPurchasesRepository purchasesRepository, IProductsRepository productsRepository)
        {
            _purchasesRepository = purchasesRepository;
            _productsRepository = productsRepository;
        }

        [HttpGet]
        [Route("GetPurchases")] //api/Purchases/GetPurchases
        public async Task<IActionResult> GetPurchases()
        {
            try
            {
                return Ok(await _purchasesRepository.GetPurchases());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetPurchase")] //api/Purchases/GetPurchase/id
        public async Task<IActionResult> GetPurchase([FromQuery] int id)
        {
            try
            {
                return Ok(await _purchasesRepository.GetPurchase(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("CreatePurchase")] //api/Purchases/CreatePurchase
        public async Task<IActionResult> CreatePurchase([FromBody] Purchase purchase)
        {
            try
            {
                decimal decimalValue;
                DateTime dateTimeValue;

                if (purchase == null)
                {
                    return BadRequest("La compra no puede ser nulo.");
                }

                if (!decimal.TryParse(purchase.Total.ToString(), out decimalValue))
                {
                    return BadRequest("El total tiene que ser un valor de tipo decimal.");
                }

                if (decimalValue <= 0)
                {
                    return BadRequest("El total debe ser mayor a cero.");
                }

                if (!DateTime.TryParse(purchase.Date.ToString(), out dateTimeValue))
                {
                    return BadRequest("La fecha debe ser del tipo DateTime.");
                }

                var product = await _productsRepository.GetProduct(purchase.IdProduct);

                if (product == null)
                {
                    return BadRequest("El Id del Producto ingresado no corresponde a ningún producto existente.");
                }

                return Ok(await _purchasesRepository.CreatePurchase(purchase));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("EditPurchase")] //api/Purchases/EditPurchase/id
        public async Task<IActionResult> EditPurchase([FromQuery] int id, [FromBody] Purchase purchase)
        {
            try
            {
                decimal decimalValue;
                DateTime dateTimeValue;

                if (purchase == null)
                {
                    return BadRequest("La compra no puede ser nulo.");
                }

                if (!decimal.TryParse(purchase.Total.ToString(), out decimalValue))
                {
                    return BadRequest("El total tiene que ser un valor de tipo decimal.");
                }

                if (decimalValue <= 0)
                {
                    return BadRequest("El total debe ser mayor a cero.");
                }

                if (!DateTime.TryParse(purchase.Date.ToString(), out dateTimeValue))
                {
                    return BadRequest("La fecha debe ser del tipo DateTime.");
                }

                var product = await _productsRepository.GetProduct(purchase.IdProduct);

                if (product == null)
                {
                    return BadRequest("El Id del Producto ingresado no corresponde a ningún producto existente.");
                }

                purchase.Id = id;

                return Ok(await _purchasesRepository.EditPurchase(purchase));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeletePurchase")] //api/Purchases/DeletePurchase/id
        public async Task<IActionResult> DeletePurchase([FromQuery] int id)
        {
            try
            {
                return Ok(await _purchasesRepository.DeletePurchase(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
