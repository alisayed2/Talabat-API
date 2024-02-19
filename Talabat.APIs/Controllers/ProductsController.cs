using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;

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
            var spec = new ProductWithBrandAndTypeSpecifications();
            var products = await productsRepo.GetAllWithSpecAsync(spec);
            return Ok(products);
        }

        // Get Product By Id End point
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var spec = new ProductWithBrandAndTypeSpecifications(id);
            var product = await productsRepo.GetByIdWithSpecAsync(spec);
            return Ok(product);
        }
    }
}
