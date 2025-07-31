using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Dtos;
using TalabatProject.Core;
using TalabatProject.Core.Entity;
using TalabatProject.Core.Interfaces;
using TalabatProject.Repository;
using TalabatProject.Repository.Data;
using TalabatProject.Repository.Specification;

namespace Talabat.APIs.Controllers
{

    public class ProductTypeController : ApiBaseController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductTypeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductTypeToReturnDto>>> GetAllBrand()
        {
            var types = await unitOfWork.Repository<ProductType>().GetAllWithSpecAsync(new ProductTypeSpecification());
            var typesDto = mapper.Map<IEnumerable<ProductType>, IEnumerable<ProductTypeToReturnDto>>(types);
            return Ok(typesDto);
        }
        [ProducesResponseType(typeof(ProductTypeToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductTypeToReturnDto>> GetById(int id)
        {
            var type = await unitOfWork.Repository<ProductType>().GetByIdWithSpecAsync(new ProductTypeSpecification(id));
            if (type is null)
                return NotFound(new ApiErrorResponse(404));
            var typeDto = mapper.Map<ProductType, ProductTypeToReturnDto>(type);
            return Ok(typeDto);
        }
    }
}
