using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeacherApi.DataAccess.DbContexts;
using TeacherApi.Domain.Models;

namespace Teacher.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TeachersController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async ValueTask<IActionResult> GetAll()
        {
            IList<TeacherApi.Domain.Models.Teacher> result = await _context.Teachers.ToListAsync();
            if (result is not null)
            {
                return Ok(result);
            }
            return BadRequest("NotFound");
        }
        [HttpPost]
        public async ValueTask<IActionResult> Create(TeacherApi.Domain.Dtos.TeacherDto teacherDto)
        {
            try
            {
                var teacher = new TeacherApi.Domain.Models.Teacher()
                {
                    FirstName = teacherDto.FirstName,
                    LastName = teacherDto.LastName,
                    ClassNumber = teacherDto.ClassNumber,
                    Description = teacherDto.Description,
                };
                await _context.Teachers.AddAsync(teacher);
                await _context.SaveChangesAsync();
                return Ok("Added");
            }
            catch
            {
                return BadRequest("Error!");
            }
        }
        [HttpGet]
        public async ValueTask<IActionResult> GetById(int id)
        {
            var result = await _context.Teachers.FirstOrDefaultAsync(x => x.Id == id);
            if (result is not null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(string.Empty);
            }
        }
        [HttpDelete]
        public async ValueTask<IActionResult> DeleteById(int id)
        {
            try
            {
                var result = await _context.Teachers.FirstOrDefaultAsync(x => x.Id == id);
                _context.Teachers.Remove(result);
                await _context.SaveChangesAsync();
                return Ok(true);
            }
            catch
            {
                return NotFound("NotFound!");
            }
        }
    }
}
