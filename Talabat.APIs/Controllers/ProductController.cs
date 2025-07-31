using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Talabat.APIs.Dtos;
using Talabat.APIs.Helper;
using TalabatProject.Core;
using TalabatProject.Core.Entity;
using TalabatProject.Core.Interfaces;
using TalabatProject.Repository;
using TalabatProject.Repository.Specification;

namespace Talabat.APIs.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductController : ApiBaseController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<Pagination<IReadOnlyList<ProductToReturnDto>>>> GetAllProducts([FromQuery]ProductSpecParams productSpecParams)
        {
            var products = await unitOfWork.Repository<Product>().GetAllWithSpecAsync(new ProductSpecification(productSpecParams));
            var productDto = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
            var CountAsync = new ProductWithFilterationForCountSpecification(productSpecParams);
            var count = await unitOfWork.Repository<Product>().GetTotalCountAsync(CountAsync);
            return Ok(new Pagination<ProductToReturnDto>(productSpecParams.PageIndex, productSpecParams.PageSize, count, productDto));
        }
        [ProducesResponseType(typeof(ProductToReturnDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse),StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProductById(int id)
        {
            var product = await unitOfWork.Repository<Product>().GetByIdWithSpecAsync(new ProductSpecification(id));
            if (product == null)
                return NotFound(new ApiErrorResponse(404));
            var productDto = mapper.Map<Product, ProductToReturnDto>(product);
            return Ok(productDto);
            
        }
    }
}
