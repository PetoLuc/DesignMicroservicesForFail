using Microsoft.AspNetCore.Mvc;
using Model;

namespace ProductApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("GetProducts")]
        public ActionResult<List<Product>> GetProducts()
        {
            return _productService.GetProducts();
        }

        [HttpGet()]
        public ActionResult Status()
        {
            return Ok("OK");
        }
    }
}



