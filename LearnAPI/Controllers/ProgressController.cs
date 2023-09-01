using LearnAPI.Data;
using LearnAPI.Model.Learn;
using LearnAPI.Model.Learn.Request;
using LearnAPI.Model.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LearnAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgressController : ControllerBase
    {
        private readonly LearnDbContext _context;
        private readonly IConfiguration _configuration;

        public ProgressController(LearnDbContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }

        /// <summary>
        /// აღნიშნავს საგანს მომხმარებლისთვის დასრულებულად.
        /// </summary>
        /// <param name="request">დასრულების მოთხოვნა, რომელიც შეიცავს მომხმარებლის და საგნის ინფორმაციას.</param>
        [HttpPost("complete-subject"), Authorize]
        public async Task<IActionResult> CompleteSubject([FromBody] CompletionRequest request)
        {
            return await Complete(request.UserId, request.SubjectId, null, null);
        }

        /// <summary>
        /// აღნიშნავს კურსს მომხმარებლისთვის დასრულებულად.
        /// </summary>
        /// <param name="request">დასრულების მოთხოვნა, რომელიც შეიცავს მომხმარებლის და კურსის ინფორმაციას.</param>
        [HttpPost("complete-course"), Authorize]
        public async Task<IActionResult> CompleteCourse([FromBody] CompletionRequest request)
        {
            return await Complete(request.UserId, null, request.CourseId, null);
        }

        /// <summary>
        /// აღნიშნავს დონეს დასრულებულად მომხმარებლისთვის.
        /// </summary>
        /// <param name="request">დასრულების მოთხოვნა, რომელიც შეიცავს მომხმარებლის და დონის ინფორმაციას.</param>
        [HttpPost("complete-level"), Authorize]
        public async Task<IActionResult> CompleteLevel([FromBody] CompletionRequest request)
        {
            return await Complete(request.UserId, null, null, request.LevelId);
        }

        private async Task<IActionResult> Complete(int userId, int? subjectId, int? courseId, int? levelId)
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value; //JWT id ჩეკავს
            var JWTRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value; //JWT Role

            var user = await GetUserWithProgress(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            if (UserId != user.UserId.ToString())
            {
                if (JWTRole != "admin")
                {
                    return BadRequest("Authorize invalid");
                }
            }

            if (user.Progress == null)
            {
                user.Progress = new ProgressModel();
            }

            if (subjectId.HasValue)
            {
                user.Progress.LastCompletedSubjectId = subjectId;

                var subject = await _context.Subjects.FirstOrDefaultAsync(u => u.SubjectId == subjectId);

                var newenroll = new CourseEnrollmentModel
                {
                    UserId = userId,
                    CourseId = subject.CourseId
                };

                _context.CourseEnroll.Add(newenroll);
            }

            if (courseId.HasValue)
            {
                user.Progress.LastCompletedCourseId = courseId;
                user.Progress.LastCompletedSubjectId = null;

                var enrollercourse = await _context.CourseEnroll.FirstOrDefaultAsync(u => u.UserId == userId && u.CourseId == courseId);

                _context.CourseEnroll.Remove(enrollercourse);
            }

            if (levelId.HasValue)
            {
                user.Progress.CurrentLevelId = levelId;
                user.Progress.LastCompletedSubjectId = null;
                user.Progress.LastCompletedCourseId = null;
            }

            user.LastActivity = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok("Completed successfully");
        }

        private async Task<UserModel> GetUserWithProgress(int userId)
        {
            var user = await _context.Users
                .Include(u => u.Progress)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            return user;
        }
    }
}
