using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShopDemoApp.Core.Contracts;
using WebShopDemoApp.Core.Models;

namespace WebShopDemoApp.Controllers
{
    /// <summary>
    /// Web shop products
    /// </summary>
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService productService;

        public ProductController(IProductService _productService)
        {
            this.productService = _productService;
        }



        /// <summary>
        /// List all products
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var products = await productService.GetAll();
            ViewData["Title"] = "Products";

            return View(products);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewData["Title"] = "Add new product";

            return View(new ProductDto());
        }


        [HttpPost]
        public async Task<IActionResult> Add(ProductDto model)
        {
            ViewData["Title"] = "Add new product";

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await productService.Add(model);  

            return RedirectToAction(nameof(Index));
        }

        //Delete
        [HttpPost]
        public async Task<IActionResult> Delete([FromForm]Guid id)
        {
            await productService.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
