using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDemoApp.Core.Contracts;
using WebShopDemoApp.Core.Models;

namespace WebShopDemoApp.Core.Services
{
    /// <summary>
    /// Manupulate product data
    /// </summary>
    public class ProductService : IProductService
    {

        private readonly IConfiguration config;

        /// <summary>
        /// IoC
        /// </summary>
        /// <param name="_config">Application configuration</param>
        public ProductService(IConfiguration _config)
        {
            config = _config;
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns>List of products</returns>
        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            string dataPath = config.GetSection("Datafiles:Products").Value;
            string data = await File.ReadAllTextAsync(dataPath);

            return JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(data);

            //string dataPath = config.GetValue();
        }
    }
}
