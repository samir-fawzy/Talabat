using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.APIs.Dtos;
using TalabatProject.Core;
using TalabatProject.Core.Entity;
using TalabatProject.Core.Interfaces;
using TalabatProject.Repository;
using TalabatProject.Repository.Data;
using TalabatProject.Repository.Specification;

namespace Talabat.APIs.Controllers
{
    public class BrandController : ApiBaseController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public BrandController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BrandToReturnDto>>> GetAllBrand()
        {
            var brands = await unitOfWork.Repository<ProductBrand>().GetAllWithSpecAsync(new BrandSpecification());
            var brandsDto = mapper.Map<IEnumerable<ProductBrand>,IEnumerable<BrandToReturnDto>>(brands);
            return Ok(brandsDto);
        }
        [ProducesResponseType(typeof(BrandToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<BrandToReturnDto>> GetById(int id)
        {
            var brand = await unitOfWork.Repository<ProductBrand>().GetByIdWithSpecAsync(new BrandSpecification(id));
            if (brand is null)
                return NotFound(new ApiErrorResponse(404));
            var brandDto = mapper.Map<ProductBrand, BrandToReturnDto>(brand);
            return Ok(brandDto);
        }
    }
}
