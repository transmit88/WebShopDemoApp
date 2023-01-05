using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShopDemoApp.Core.Constants;
using WebShopDemoApp.Core.Contracts;
using WebShopDemoApp.Core.Models;

namespace WebShopDemoApp.Controllers
{
    /// <summary>
    /// Web shop products
    /// </summary>
    public class ProductController : BaseController
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
        [Authorize(Roles = $"{RoleConstants.Manager}, {RoleConstants.Supervisor}")]
        public IActionResult Add()
        {
            ViewData["Title"] = "Add new product";

            return View(new ProductDto());
        }


        [HttpPost]
        [Authorize(Roles = $"{RoleConstants.Manager}, {RoleConstants.Supervisor}")]
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
        [Authorize(Roles = RoleConstants.Supervisor)]
        public async Task<IActionResult> Delete([FromForm]Guid id)
        {
            await productService.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
