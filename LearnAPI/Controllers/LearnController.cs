using LearnAPI.Data;
using LearnAPI.Model.Learn;
using LearnAPI.Model.Learn.Request;
using LearnAPI.Model.Learn.Test;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

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


        [HttpGet("Levels")]
        public async Task<ActionResult<IEnumerable<LevelModel>>> Levels()
        {
            var levels = await _context.Levels
                .Include(u => u.Courses)
                .ToListAsync();

            return Ok(levels);
        }

        [HttpGet("Level/{levelid}")]
        public async Task<ActionResult<LevelModel>> Level(int levelid)
        {
            var level = await _context.Levels
                .Include(u => u.Courses)
                .FirstOrDefaultAsync(u => u.LevelId == levelid);

            return Ok(level);
        }



        [HttpPost("Levels"), Authorize(Roles = "admin")]
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
        [HttpPut("Levels/{levelid}"), Authorize(Roles = "admin")]
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


        [HttpDelete("Levels/{levelid}"), Authorize(Roles = "admin")]
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



        [HttpGet("Courses")]
        public async Task<IActionResult> Courses()
        {

            var courses = await _context.Courses
                .Include(u => u.Level)
                .Include(u => u.Subjects)
                .Include(u => u.Enrollments)
                .ToListAsync();


            return Ok(courses);
        }

        [HttpGet("Course/{courseid}")]
        public async Task<IActionResult> Course(int courseid)
        {

            var courses = await _context.Courses
                .Include(u => u.Level)
                .Include(u => u.Subjects)
                .Include(u => u.Enrollments)
                .FirstOrDefaultAsync(u => u.CourseId == courseid);


            return Ok(courses);
        }


        [HttpPost("Course"), Authorize(Roles = "admin")]
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


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }

            return Ok(course);
        }



        [HttpPut("Courses/{courseid}"), Authorize(Roles = "admin")]
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


        [HttpDelete("Courses/{courselid}"), Authorize(Roles = "admin")]
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



        //Subjects//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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


        [HttpGet("Subjects")]
        public async Task<ActionResult<IEnumerable<SubjectModel>>> Subjects()
        {

            var subjects = await _context.Subjects
                .Include(u => u.Tests)
                .ToListAsync();

            return Ok(subjects);
        }

        [HttpGet("Subject/{subjectid}")]
        public async Task<IActionResult> Subject(int subjectid)
        {

            var subject = await _context.Subjects
                .Include(u => u.Tests)
                .FirstOrDefaultAsync(u => u.CourseId == subjectid);


            return Ok(subject);
        }


        [HttpPost("Subject"), Authorize(Roles = "admin")]
        public async Task<IActionResult> AddSubject(NewSubjectModel newsubject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_context.Subjects.Any(u => u.SubjectName == newsubject.SubjectName))
            {
                return BadRequest("Course Already Exists");
            }

            var subject = new SubjectModel
            {
                SubjectName = newsubject.SubjectName,
                Description = newsubject.Description,
                LogoURL =newsubject.LogoURL,
                CourseId = newsubject.CourseId,
            };


            _context.Subjects.Add(subject);


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(subject);
        }



        [HttpPut("Subjects/{subjectid}"), Authorize(Roles = "admin")]
        public async Task<IActionResult> EditSubject(NewSubjectModel newsubject, int subjectid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var subject = await _context.Subjects.FirstOrDefaultAsync(u => u.SubjectId == subjectid);

            if (subject == null)
            {
                return NotFound("level Not been Found");
            }

            subject.SubjectName = newsubject.SubjectName;
            subject.Description = newsubject.Description;
            subject.LogoURL = newsubject.LogoURL;   

            await _context.SaveChangesAsync();

            return Ok(subject);
        }


        [HttpDelete("Subjects/{subjectid}"), Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteSubject(int subjectid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var subject = await _context.Subjects.FirstOrDefaultAsync(u => u.SubjectId == subjectid);

            if (subject == null)
            {
                return NotFound("level Not been Found");
            }

            _context.Subjects.Remove(subject);

            await _context.SaveChangesAsync();

            return Ok("Removed");
        }



        //Tests//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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


        
        [HttpGet("Tests")]
        public async Task<ActionResult<IEnumerable<TestModel>>> GetTests()
        {
            return await _context.Tests.Include(t => t.Answers).ToListAsync();
        }

        [HttpGet("Tests/{id}")]
        public async Task<ActionResult<TestModel>> GetTest(int id)
        {
            var test = await _context.Tests.Include(t => t.Answers).FirstOrDefaultAsync(t => t.TestId == id);

            if (test == null)
            {
                return NotFound();
            }

            return test;
        }

        [HttpPost("Tests")]
        public async Task<ActionResult<TestModel>> PostTest(NewTestModel test)
        {
            var Test = new TestModel
            {
                Question = test.Question,
                Hint = test.Hint,
            };


            _context.Tests.Add(Test);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTest), new { id = Test.TestId }, Test);
        }

        [HttpPut("Tests/{id}")]
        public async Task<IActionResult> PutTest(int id, TestModel test)
        {
            if (id != test.TestId)
            {
                return BadRequest();
            }

            _context.Entry(test).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("Tests/{id}")]
        public async Task<IActionResult> DeleteTest(int id)
        {
            var test = await _context.Tests.FindAsync(id);

            if (test == null)
            {
                return NotFound();
            }

            _context.Tests.Remove(test);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        /////////////////////ANSWER
        ///
        [HttpPost("{testId}/answers")]
        public async Task<ActionResult<TestModel>> AddAnswerToTest(int testId, TestAnswerModel answer)
        {
            var test = await _context.Tests.Include(t => t.Answers).FirstOrDefaultAsync(t => t.TestId == testId);

            if (test == null)
            {
                return NotFound();
            }

            if (test.Answers == null)
            {
                test.Answers = new List<TestAnswerModel>();
            }

            test.Answers.Add(answer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTest), new { id = test.TestId }, test);
        }

    }
}
