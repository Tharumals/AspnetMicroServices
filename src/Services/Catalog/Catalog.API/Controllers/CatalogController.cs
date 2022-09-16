using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepositiry _repositiry;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepositiry repositiry, ILogger<CatalogController> logger)
        {
            _repositiry = repositiry;
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var product = await _repositiry.GetProducts();
            return Ok(product);
        }

        [HttpGet("{id:length{24}}", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductById(string id)
        {
            var product = await _repositiry.GetProduct(id);
            if (product == null)
            {
                _logger.LogError($"Product with id:{id}, not found.");
                return NotFound();
            }
            return Ok(product);
        }

        [Route("[action]/{category}", Name = "GetProductByCategory")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProcuctByCategory( string category)
        {
            var product = await _repositiry.GetProductByCatagory(category);
            return Ok(product);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]

        public async Task<ActionResult> UpdateProduct([FromBody] Product product)
        {
            return Ok(await _repositiry.UpdateProduct(product));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]

        public async Task<ActionResult> CreateProduct([FromBody] Product product)
        {
            await _repositiry.CreateProduct(product);

            return CreatedAtRoute("GetProduct", new { id = product.Id}, product);
        }

        [HttpDelete("{Id:length{24}}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]

        public async Task<ActionResult> DeleteProductById(string id)
        {
            return Ok(await _repositiry.DeleteProduct(id));
        }
    }
}
