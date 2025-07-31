using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TalabatProject.Repository.Data;

namespace Talabat.APIs.Controllers
{
    public class BuggyController : ApiBaseController
    {
        private readonly StoreContext context;

        public BuggyController(StoreContext context)
        {
            this.context = context;
        }
        [HttpGet("notfound")]
        public ActionResult GetUserNotFuond()
        {
            var product = context.Products.Find(42);
            if (product == null)
                return NotFound(new ApiErrorResponse(404));
            return Ok(product);
        }
        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var product = context.Products.Find(42);
            var productToReturn = product.ToString();

            return Ok(productToReturn);
        }
        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiErrorResponse(400));
        }
        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadRequest(int id)
        {
            return Ok();
        }
    }
}
