using DirectorApi.Domain.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentApi.DataAccess.DbContexts;
using StudentApi.Domain.Dtos;

namespace Student.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public StudentsController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        [HttpGet]
        public async ValueTask<IActionResult> GetAll()
        {
            IEnumerable<StudentApi.Domain.Models.Student> directors = await _appDbContext.Students.ToListAsync();
            if (directors is not null)
            {
                return Ok(directors);
            }
            return BadRequest("Errorr!");
        }
        [HttpPost]
        public async ValueTask<IActionResult> Create(StudentDto student)
        {
            try
            {
                var std = new StudentApi.Domain.Models.Student()
                {
                    Name = student.Name,
                    LastName = student.LastName,
                    Age = student.Age,
                    JSHO = student.JSHO
                };
                await _appDbContext.Students.AddAsync(std);
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
                var result = await _appDbContext.Students.FirstOrDefaultAsync(x => x.Id == id);

                _appDbContext.Students.Remove(result);
                await _appDbContext.SaveChangesAsync();
                return Ok(true);
            }
            catch
            {
                return BadRequest(false);
            }
        }

        [HttpGet]
        public async ValueTask<StudentApi.Domain.Models.Student> GetById(int id)
        {
            var director = await _appDbContext.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (director is not null)
            {
                return director;
            }
            return new StudentApi.Domain.Models.Student();
        }
    }
}

