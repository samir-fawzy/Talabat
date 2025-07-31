using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatProject.Core.Entity;

namespace TalabatProject.Repository.Specification
{
    public class ProductWithFilterationForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFilterationForCountSpecification(ProductSpecParams productSpecParams)
        : base(P =>
        (string.IsNullOrEmpty(productSpecParams.SearchByName) || P.Name.ToLower().Contains(productSpecParams.SearchByName)) &&
        (!productSpecParams.BrandId.HasValue || P.ProductBrandId == productSpecParams.BrandId) &&
        (!productSpecParams.TypeId.HasValue || P.ProductTypeId == productSpecParams.TypeId)
        )
        {

        }
    }
}
