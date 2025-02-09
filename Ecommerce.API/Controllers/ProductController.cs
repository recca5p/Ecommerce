using Contract.DTOs.Request;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;

namespace Ecommerce.API.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private ILogger<ProductController> _logger;

        public ProductController(IServiceManager serviceManager, ILogger<ProductController> logger)
        {
            _serviceManager = serviceManager;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            try
            {
                var products = await _serviceManager.ProductService.GetAllAsync();
                
                return Ok(products);
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occured while getting all products: {e.Message}");
                throw;
            }
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(long id)
        {
            try
            {
                var product = await _serviceManager.ProductService.GetByIdAsync(id);
                
                return Ok(product);
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occured while getting a product: {e.Message}");
                throw;
            }
        }
        
        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<ActionResult<Product>> Create(ProductForCreationDto productForCreation)
        {
            try
            {
                await _serviceManager.ProductService.CreateAsync(productForCreation);
                
                return Created();
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occured while create a product: {e.Message}");
                throw;
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("id")]
        public async Task<ActionResult<Product>> Update(long id, ProductForUpdateDto productForUpdate)
        {
            try
            {
                await _serviceManager.ProductService.UpdateAsync(id, productForUpdate);

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occured while create a product: {e.Message}");
                throw;
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("id")]
        public async Task<ActionResult<Product>> Delete(long id)
        {
            try
            {
                await _serviceManager.ProductService.DeleteAsync(id);
                
                return Created();
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occured while create a product: {e.Message}");
                throw;
            }
        }
    }