using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace shopicruit_w16
{
    public class Program
    {
        // The target JSON URL to read.
        static string targetURL = "http://shopicruit.myshopify.com/products.json";
        // The types of products we want.
        static List<string> weWant = new List<string>() { "Lamp", "Wallet" };

        static void Main(string[] args)
        {
            // Read the target URL.
            WebClient client = new WebClient();
            Stream stream = client.OpenRead(targetURL);
            StreamReader reader = new StreamReader(stream);
            String rawJson = reader.ReadToEnd();

            // Deserialize the JSON into our DTOs.
            Dictionary<string, List<Product>> rawProducts = JsonConvert.DeserializeObject<Dictionary<string, List<Product>>>(rawJson);
            List<Product> products = rawProducts["products"];

            // Go through the products and check for products we want.
            // Tally up the total price of the products we want.
            double total = 0;
            foreach (Product p in products)
            {
                // If the product type is something we want, loop through its variants.
                if (weWant.Contains(p.product_type))
                {
                    List<ProductVariant> variants = p.variants;
                    foreach (ProductVariant v in variants)
                    {
                        total += v.price;
                    }
                }
            }

            Console.WriteLine(String.Format("This will cost you ${0:0.00} (before taxes).", total));

            Console.WriteLine("Press the return key to exit.");
            Console.ReadLine();
        }
    }
}
