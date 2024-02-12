using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;

namespace Talabat.APIs.Controllers
{

    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> productsRepo;

        public ProductsController(IGenericRepository<Product> _productsRepo)
        {
            productsRepo = _productsRepo;
        }

        // Get All Products End Point
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await productsRepo.GetAllAsync();
            return Ok(products);
        }

        // Get Product By Id End point
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await productsRepo.GetByIdAsync(id);
            return Ok(product);
        }
    }
}
