using Contract.DTOs.Request;
using Domain.Entities;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(long ID)
        {
            try
            {
                var product = _serviceManager.ProductService.GetByIdAsync(ID);
                
                return Ok(product);
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occured while getting a product: {e.Message}");
                throw;
            }
        }
        
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
        
        [HttpPut("id")]
        public async Task<ActionResult<Product>> Update(long ID, ProductForUpdateDto productForUpdate)
        {
            try
            {
                await _serviceManager.ProductService.UpdateAsync(ID, productForUpdate);

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occured while create a product: {e.Message}");
                throw;
            }
        }
        
        [HttpDelete("id")]
        public async Task<ActionResult<Product>> Delete(ProductForCreationDto productForCreation)
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
    }