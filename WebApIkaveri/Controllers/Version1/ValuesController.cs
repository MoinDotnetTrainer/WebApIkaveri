using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApIkaveri.Models;
using Asp.Versioning;

namespace WebApIkaveri.Controllers.Version1
{
    [ApiController]

    [ApiVersion("1.0")]

    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class ValuesController : ControllerBase
    {
        private readonly AppDb _context;

        public ValuesController(AppDb context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> GetProducts()
        {
            var res = await (from s in _context.Products where s.ProductID > 2 select s).ToListAsync();

            return Ok(res);
        }
    }
}
