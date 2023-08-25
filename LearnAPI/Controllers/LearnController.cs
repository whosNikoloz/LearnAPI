using LearnAPI.Data;
using LearnAPI.Model.Learn;
using LearnAPI.Model.Learn.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearnAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LearnController : ControllerBase
    {
        private readonly LearnDbContext _context;
        private readonly IConfiguration _configuration;

        public LearnController(LearnDbContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }


        [HttpPost("Levels")]
        public async Task<IActionResult> AddLevel(NewLevelModel newLevel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_context.Levels.Any(u => u.LevelName == newLevel.LevelName))
            {
                return BadRequest("Level Already Exists");
            }

            var level = new LevelModel
            {
                LevelName = newLevel.LevelName,
                Description = newLevel.Description,
                LogoURL = newLevel.LogoURL,
            };

            _context.Levels.Add(level);

            await _context.SaveChangesAsync();

            return Ok(level);
        }
        [HttpPut("Levels/{levelid}")]
        public async Task<IActionResult> EditLevel(NewLevelModel newLevel, int levelid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var level = await _context.Levels.FirstOrDefaultAsync(u => u.LevelId == levelid);

            if (level == null)
            {
                return NotFound("level Not been Found");
            }

            level.LevelName = newLevel.LevelName;
            level.LogoURL = newLevel.LogoURL;
            level.Description = newLevel.Description;

            await _context.SaveChangesAsync();

            return Ok(level);
        }


        [HttpDelete("Levels/{levelid}")]
        public async Task<IActionResult> DeleteLevel(int levelid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var level = await _context.Levels.FirstOrDefaultAsync(u => u.LevelId == levelid);

            if (level == null)
            {
                return NotFound("level Not been Found");
            }

            _context.Levels.Remove(level);

            await _context.SaveChangesAsync();

            return Ok("Removed");
        }

        //Course//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        


        [HttpPost("Course")]
        public async Task<IActionResult> AddCourse(NewCourseModel newCourseModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_context.Courses.Any(u => u.CourseName == newCourseModel.CourseName))
            {
                return BadRequest("Course Already Exists");
            }

            var course = new CourseModel
            {
                CourseName = newCourseModel.CourseName,
                Description = newCourseModel.Description,
                LevelId = newCourseModel.LevelId
            };


            _context.Courses.Add(course);

            await _context.SaveChangesAsync();

            return Ok(course);
        }



        [HttpPut("Courses/{courseid}")]
        public async Task<IActionResult> EditCourse(NewCourseModel newcourse, int courseid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var course = await _context.Courses.FirstOrDefaultAsync(u => u.CourseId == courseid);

            if (course == null)
            {
                return NotFound("level Not been Found");
            }

            course.CourseName = newcourse.CourseName;
            course.Description = newcourse.Description;
            course.LevelId = newcourse.LevelId;

            await _context.SaveChangesAsync();

            return Ok(course);
        }


        [HttpDelete("Courses/{courselid}")]
        public async Task<IActionResult> DeleteCourse(int courseid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var course = await _context.Courses.FirstOrDefaultAsync(u => u.CourseId == courseid);

            if (course == null)
            {
                return NotFound("level Not been Found");
            }

            _context.Courses.Remove(course);

            await _context.SaveChangesAsync();

            return Ok("Removed");
        }


    }
}
