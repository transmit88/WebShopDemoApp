using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShopDemoApp.Core.Contracts;

namespace WebShopDemoApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductService productService;

        public ProductController(IProductService _productService)
        {
            this.productService = _productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await productService.GetAll());
        }
    }
}
