using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDemoApp.Core.Contracts;
using WebShopDemoApp.Core.Data.Common;
using WebShopDemoApp.Core.Data.Models;
using WebShopDemoApp.Core.Models;

namespace WebShopDemoApp.Core.Services
{
    /// <summary>
    /// Manupulate product data
    /// </summary>
    public class ProductService : IProductService
    {

        private readonly IConfiguration config;

        private readonly IRepository repo;

        /// <summary>
        /// IoC
        /// </summary>
        /// <param name="_config">Application configuration</param>
        public ProductService(
            IConfiguration _config,
            IRepository _repo)
        {
            config = _config;
            repo = _repo;
        }

        /// <summary>
        /// Add new product
        /// </summary>
        /// <param name="productDto">Product model</param>
        /// <returns></returns>
        public async Task Add(ProductDto productDto)
        {
            var product = new Product()
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Quantity = productDto.Quantity
            };

            await repo.AddAsync(product); // Add in DB
            await repo.SaveChangesAsync();// Save Changes
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns>List of products</returns>
        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            return await repo.AllReadonly<Product>()
                .Select(p => new ProductDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity
                })
                .ToListAsync();


            //string dataPath = config.GetSection("Datafiles:Products").Value;
            //string data = await File.ReadAllTextAsync(dataPath);

            //return JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(data);

            //string dataPath = config.GetValue();
        }
    }
}
