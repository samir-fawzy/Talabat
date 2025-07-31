using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatProject.Core.Entity;

namespace TalabatProject.Repository.Specification
{
    public class ProductTypeSpecification : BaseSpecification<ProductType>
    {
        public ProductTypeSpecification() { }
        public ProductTypeSpecification(int id):base(T => T.Id == id)
        {
            
        }
    }
}
