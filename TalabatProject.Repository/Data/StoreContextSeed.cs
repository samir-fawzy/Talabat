using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TalabatProject.Core.Entity;
using TalabatProject.Core.Entity.Order_Aggregatoin;

namespace TalabatProject.Repository.Data
{
    public class StoreContextSeed
    {
        public static async Task StoreAsync(StoreContext dbContext)
        {
            // return true if the table has data
            // return false if the table is empty
            if (!await dbContext.ProductBrands.AnyAsync())
            {
                var brandsJson = File.ReadAllText("../TalabatProject.Repository/Data/DataSeed/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsJson);

                if(brands?.Count > 0)
                {
 
                       await dbContext.ProductBrands.AddRangeAsync(brands);
                       await dbContext.SaveChangesAsync();
                }
            }
            if (!await dbContext.ProductTypes.AnyAsync())
            {
                var typesJson = File.ReadAllText("../TalabatProject.Repository/Data/DataSeed/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesJson);

                if (types?.Count > 0)
                {

                    await dbContext.ProductTypes.AddRangeAsync(types);
                    await dbContext.SaveChangesAsync();
                }
            }
            if (!await dbContext.Products.AnyAsync())
            {
                var productsJson = File.ReadAllText("../TalabatProject.Repository/Data/DataSeed/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsJson);

                if (products?.Count > 0)
                {

                    await dbContext.Products.AddRangeAsync(products);
                    await dbContext.SaveChangesAsync();
                }
            }
            if (!await dbContext.DeliveryMethods.AnyAsync())
            {
                var MethodsJson = File.ReadAllText("../TalabatProject.Repository/Data/DataSeed/delivery.json");
                var Methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(MethodsJson);

                if (Methods?.Count > 0)
                {

                    await dbContext.DeliveryMethods.AddRangeAsync(Methods);
                    await dbContext.SaveChangesAsync();
                    
                }
            }

        }
    }
}
