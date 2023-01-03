using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopDemoApp.Core.Models
{
    public class ProductDto
    {
        /// <summary>
        /// Prodct identified
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Product name
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Product price
        /// </summary>
        [Range(typeof(decimal), "0.1", "100", ConvertValueInInvariantCulture = true)]
        public decimal Price { get; set; }

        /// <summary>
        /// Product in stock
        /// </summary>
        [Range(1, int.MaxValue)]
        public int  Quantity { get; set; }

    }
}
