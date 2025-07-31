using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatProject.Core.Entity;

namespace TalabatProject.Repository.Specification
{
    public class BrandSpecification : BaseSpecification<ProductBrand>
    {
        public BrandSpecification() 
        {

        }
        public BrandSpecification(int id):base(B => B.Id == id)
        {

        }
    }
}
