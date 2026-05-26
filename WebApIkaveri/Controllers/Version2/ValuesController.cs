using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApIkaveri.Models;

namespace WebApIkaveri.Controllers.Version2
{
    [ApiController]

    [ApiVersion("2.0")]

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

            // return await _context.Products.ToListAsync();
            return Ok(await _context.Products.ToListAsync());        //200 sttaus , return successfully 
        }

    }
}
