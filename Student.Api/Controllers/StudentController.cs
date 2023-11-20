using DirectorApi.DataAccess.ApplicationDbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Student.Api.Controllers
{
    [Route("api/[controller]/action")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public StudentController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        [HttpGet]
        public async ValueTask<IActionResult> GetAll()
        {
            IEnumerable<> directors = await _appDbContext.Directors.ToListAsync();
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
        [HttpDelete]
        public async ValueTask<IActionResult> DeleteById(int id)
        {
            try
            {
                var result = await _appDbContext.Directors.FirstOrDefaultAsync(x => x.Id == id);

                _appDbContext.Directors.Remove(result);
                await _appDbContext.SaveChangesAsync();
                return Ok(true);
            }
            catch
            {
                return BadRequest(false);
            }
        }

        [HttpGet]
        public async ValueTask<DirectorApi.Domain.Models.Director> GetById(int id)
        {
            var director = await _appDbContext.Directors.FirstOrDefaultAsync(x => x.Id == id);
            if (director is not null)
            {
                return director;
            }
            return new DirectorApi.Domain.Models.Director();
        }
    }
}
}
