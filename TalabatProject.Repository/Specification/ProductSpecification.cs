using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatProject.Core.Entity;

namespace TalabatProject.Repository.Specification
{
    public class ProductSpecification : BaseSpecification<Product>
    {

        public ProductSpecification(ProductSpecParams productSpecParams)
            :base(P =>
            (string.IsNullOrEmpty(productSpecParams.SearchByName) || P.Name.ToLower().Contains(productSpecParams.SearchByName)) &&
            (!productSpecParams.BrandId.HasValue || P.ProductBrandId == productSpecParams.BrandId) &&
            (!productSpecParams.TypeId.HasValue || P.ProductTypeId == productSpecParams.TypeId)
            )
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);


            if(!string.IsNullOrEmpty(productSpecParams.Sort))
            {
                switch(productSpecParams.Sort)
                {
                    case "PriceAsc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "PriceDesc":
                        AddOrderByDescending(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            }
            // 100 products
            // page size 10 
            // page index = 5
            // skip 10 *(5 - 1)
            ApplyPagination(productSpecParams.PageSize * (productSpecParams.PageIndex - 1), productSpecParams.PageSize);
            
        }
        public ProductSpecification(int id) : base(P => P.Id == id)
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);
        }
    }
}
