using DirectorApi.DataAccess.ApplicationDbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Director.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DirectorsController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public DirectorsController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        [HttpGet]
        public async ValueTask<IActionResult> GetAll()
        {
            return Ok(await _appDbContext.Directors.ToListAsync());
        }
    }
}
