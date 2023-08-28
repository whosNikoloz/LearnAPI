using LearnAPI.Data;
using LearnAPI.Model.Learn;
using LearnAPI.Model.Learn.Request;
using LearnAPI.Model.Learn.Test;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;

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
                .Include(u => u.LearnMaterials)
                .Include(u => u.Tests)
                .Include(u => u.Course)
                .ToListAsync();

            return Ok(subjects);
        }

        [HttpGet("Subject/{subjectid}")]
        public async Task<IActionResult> Subject(int subjectid)
        {

            var subject = await _context.Subjects
                .Include(u => u.LearnMaterials)
                .Include(u => u.Tests)
                .Include(u => u.Course)
                .FirstOrDefaultAsync(u => u.CourseId == subjectid);


            return Ok(subject);
        }


        [HttpPost("Subject")]
        public async Task<IActionResult> AddSubject(NewSubjectModel newsubject,string coursename)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var course = await _context.Courses.FirstOrDefaultAsync(u => u.CourseName == coursename);

            if (course == null)
            {
                return NotFound("Course Already Exists");
            }

            if (_context.Subjects.Any(u => u.SubjectName == newsubject.SubjectName && u.CourseId == course.CourseId))
            {
                return BadRequest("Subject Already Exists");
            }

            

            var subject = new SubjectModel
            {
                SubjectName = newsubject.SubjectName,
                Description = newsubject.Description,
                LogoURL =newsubject.LogoURL,
                CourseId = course.CourseId,
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
                SubjectId = test.SubjectId
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


        [HttpGet("answers/{testid}")]
        public async Task<ActionResult<TestModel>> GetAnswers(int testId)
        {
            var test = await _context.Tests
                .Include(t => t.Answers)
                .FirstOrDefaultAsync(t => t.TestId == testId);

            return CreatedAtAction(nameof(GetTest), new { id = test.TestId }, test);
        }


        [HttpPost("{testId}/answers")]
        public async Task<ActionResult<TestModel>> AddAnswerToTest(int testId, NewTestAnswerModel answer)
        {
            var test = await _context.Tests
                .Include(t => t.Answers)
                .FirstOrDefaultAsync(t => t.TestId == testId);

            if (test == null)
            {
                return NotFound();
            }

            if (test.Answers == null)
            {
                test.Answers = new List<TestAnswerModel>();
            }

            var Answer = new TestAnswerModel
            {
                Option = answer.Option,
                IsCorrect = answer.IsCorrect,
                TestId = testId,
            }; 

            _context.TestAnswers.Add(Answer);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Log the exception or handle it in a way that makes sense for your application
                // You might inform the user about the concurrency issue and prompt for action
            }

            return CreatedAtAction(nameof(GetTest), new { id = test.TestId }, test);
        }

        [HttpDelete("answers/{testid}")]
        public async Task<IActionResult> DeleteAnswers(int answerid)
        {
            var answer = await _context.TestAnswers
                .FirstOrDefaultAsync(t => t.AnswerId == answerid);

            if(answer == null)
            {
                return NotFound();
            }

            return Ok("Deleted");
        }

        //Learn-test//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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


        [HttpGet("LearnMaterials")]
        public async Task<ActionResult<IEnumerable<LearnModel>>> GetLearns()
        {
            return await _context.Learn.Include(t => t.Question).ToListAsync();
        }


        [HttpGet("LearnMaterial/{id}")]
        public async Task<ActionResult<LearnModel>> GetLearn(int id)
        {
            var learn = await _context.Learn.Include(t => t.Question).FirstOrDefaultAsync(t => t.LearnId == id);

            if (learn == null)
            {
                return NotFound();
            }

            return learn;
        }

        [HttpPost("LearnMaterial")]
        public async Task<IActionResult> PostLearn(NewLearnModel learn,string subjectname,string coursename)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(u => u.CourseName == coursename);


            if(course == null)
            {
                return NotFound();
            }

            var subject = await _context.Subjects.FirstOrDefaultAsync(u => u.SubjectName == subjectname && u.CourseId == course.CourseId);


            if (subject == null)
            {
                return NotFound();
            }

            if (_context.Learn.Any(u => u.LearnName == learn.LearnName))
            {
                return BadRequest("Learn Already Exists");
            }

            var Learn = new LearnModel
            {
                LearnName = learn.LearnName,
                Description = learn.Description,
                SubjectId = subject.SubjectId,
                VideoId = learn.VideoId
            };


            _context.Learn.Add(Learn);
            await _context.SaveChangesAsync();

            return Ok(Learn);
        }


        [HttpPut("LearnMaterial/{id}")]
        public async Task<IActionResult> PutLearn(int id, LearnModel learn)
        {
            if (id != learn.LearnId)
            {
                return BadRequest();
            }

            _context.Entry(learn).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("LearnMaterial/{id}")]
        public async Task<IActionResult> DeleteLearn(int id)
        {
            var learn = await _context.Learn.FindAsync(id);

            if (learn == null)
            {
                return NotFound();
            }

            _context.Learn.Remove(learn);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        
    }
}
