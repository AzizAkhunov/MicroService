using DirectorApi.DataAccess.ApplicationDbContext;
using DirectorApi.Domain.Dtos;
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
            IEnumerable<DirectorApi.Domain.Models.Director> directors = await _appDbContext.Directors.ToListAsync();
            if (directors is not null)
            {
                return Ok(directors);
            }
            return BadRequest("Errorr!");
        }
        [HttpPost]
        public async ValueTask<IActionResult> Create(DirectorDto director)
        {
            try
            {
                var drc = new DirectorApi.Domain.Models.Director()
                {
                    FirstName = director.FirstName,
                    LastName = director.LastName,
                    SchoolName = director.SchoolName,
                    SchoolNumber = director.SchoolNumber
                };
                await _appDbContext.Directors.AddAsync(drc);
                await _appDbContext.SaveChangesAsync();
                return Ok(true);
            }
            catch
            {
                return BadRequest(false);
            }
        }
    }
}
