using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// This class is a DTO for each product.
namespace shopicruit_w16
{
    public class Product
    {
        public string product_type { get; set; }
        public List<ProductVariant> variants { get; set; }
    }
}
