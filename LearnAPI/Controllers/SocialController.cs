using LearnAPI.Model.Social.Request;
using LearnAPI.Model.Social;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LearnAPI.Data;
using Microsoft.EntityFrameworkCore;
using LearnAPI.Model.Learn.Test;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace LearnAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialController : ControllerBase
    {
        private readonly LearnDbContext _context;
        private readonly IConfiguration _configuration;

        public SocialController(LearnDbContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }


        // ---------- Posts ----------

        /// <summary>
        /// მიიღეთ ყველა პოსტის სია.
        /// </summary>

        [HttpGet("Posts")]
        public async Task<ActionResult<IEnumerable<PostModel>>> GetPosts([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var query = _context.Posts.Include(p => p.User).Include(p => p.Comments).AsQueryable();

            // Calculate the number of items to skip
            int skip = (page - 1) * pageSize;

            // Apply pagination
            var posts = await query.Skip(skip).Take(pageSize).ToListAsync();

            var responseList = new List<object>();

            foreach (var post in posts)
            {
                var postResponse = new
                {
                    postId = post.PostId,
                    title = post.Title,
                    content = post.Content,
                    video = post.Video,
                    picture = post.Picture,
                    subject = post.Subject,
                    createdAt = post.CreateDate,
                    user = new
                    {
                        userId = post.User.UserId,
                        firstname = post.User.FirstName,
                        lastname = post.User.LastName,
                        username = post.User.UserName,
                        picture = post.User.Picture,
                    },
                    comments = post.Comments
                };

                responseList.Add(postResponse);
            }

            return Ok(responseList);
        }


        /// <summary>
        /// მიიღეთ კონკრეტული პოსტი მისი ID-ით.
        /// </summary>
        /// <param name="postId">მოსაბრუნებელი პოსტის ID.</param>
        [HttpGet("Post/{postId}")]
        public async Task<IActionResult> GetPost(int postId)
        {
            return Ok(await _context.Posts.FirstOrDefaultAsync(u => u.PostId == postId));

        }

        /// <summary>
        /// მიიღეთ პოსტები კონკრეტული თემით.
        /// </summary>
        /// <param name="subject">პოსტების გაფილტვრის საგანი.</param>
        [HttpGet("Post/{subject}")]
        public async Task<IActionResult> PostsWithSubjeect(string subject)
        {
            return Ok(await _context.Posts.FirstOrDefaultAsync(u => u.Subject == subject));

        }

        /// <summary>
        /// შექმენით ახალი პოსტი მომხმარებლისთვის.
        /// </summary>
        /// <param name="Post">პოსტის ინფორმაციის შექმნა.</param>
        /// <param name="Userid">პოსტის შემქმნელი მომხმარებლის ID.</param>
        [HttpPost("Posts/{Userid}"), Authorize]
        public async Task<IActionResult> CreatePost(PostRequestModel Post, int Userid)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value; //JWT id ჩეკავს
            var JWTRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value; //JWT Role

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _context.Users.Include(u => u.Posts).FirstOrDefaultAsync(u => u.UserId == Userid);  //მაძლევს Users პოსტებით

            if (user == null)
            {
                return BadRequest("User Not Found");
            }
            if (userId != user.UserId.ToString())
            {
                if(JWTRole != "admin")
                {
                    return BadRequest("Authorize invalid");
                }
            }

            var NewPost = new PostModel
            {
                Title = Post.Title,
                Subject = Post.Subject,
                Content = Post.Content,
                Video = Post.Video,
                Picture = Post.Picture,
                CreateDate = DateTime.Now,
                User = user,
            };

            user.Posts ??= new List<PostModel>();
            user.Posts.Add(NewPost);

            _context.Posts.Add(NewPost);
            await _context.SaveChangesAsync();



            return Ok(NewPost);
        }

        /// <summary>
        /// არსებული პოსტის რედაქტირება.
        /// </summary>
        /// <param name="EditedPost">რედაქტირებული პოსტის ინფორმაცია.</param>
        [HttpPut("Posts"), Authorize]
        public async Task<IActionResult> EditPost(EditPostRequestModel EditedPost)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value; //JWT id ჩეკავს
            var JWTRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value; //JWT Role


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var post = await _context.Posts.Include(u => u.User).FirstOrDefaultAsync(u => u.PostId == EditedPost.PostId);  //მაძლევს Users პოსტებით

            if (post == null)
            {
                return NotFound("Post Not Found");
            }


            var user = await _context.Users.Include(u => u.Posts).FirstOrDefaultAsync(u => u.UserId == post.User.UserId);  //მაძლევს Users პოსტებით



            if (user == null)
            {
                return BadRequest("User Not Found");
            }

            if (userId != user.UserId.ToString())
            {
                if (JWTRole != "admin")
                {
                    return BadRequest("Authorize invalid");
                }
            }


            user.Posts.Remove(post);



            post.Title = EditedPost.Title;
            post.Subject = EditedPost.Subject;
            post.Content = EditedPost.Content;
            post.Video = EditedPost.Video;
            post.Picture = EditedPost.Picture;
            post.CreateDate = DateTime.Now;
            post.User = user;



            user.Posts ??= new List<PostModel>();
            user.Posts.Add(post);

            await _context.SaveChangesAsync();



            return Ok(post);
        }

        /// <summary>
        /// პოსტის წაშლა მისი ID-ით.
        /// </summary>
        /// <param name="postId">წაშლილი პოსტის ID.</param>
        [HttpDelete("Posts/{postId}"), Authorize]
        public async Task<IActionResult> DeletePost(int postId)
        {

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value; //JWT id ჩეკავს
            var JWTRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value; //JWT Role

            var post = await _context.Posts.Include(u => u.User).FirstOrDefaultAsync(u => u.PostId == postId);  //მაძლევს Users პოსტებით

            if (post == null) { return NotFound(); }

            var user = await _context.Users.Include(u => u.Posts).FirstOrDefaultAsync(u => u.UserId == post.User.UserId);  //მაძლევს Users პოსტებით

            if (userId != user.UserId.ToString())
            {
                if (JWTRole != "admin")
                {
                    return BadRequest("Authorize invalid");
                }
            }

            if (user == null) { return NotFound(); }

            user.Posts.Remove(post);


            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return Ok("Succesfully Deleted");
        }


        // ---------- Comments ----------


        /// <summary>
        /// შექმენით ახალი კომენტარი პოსტისთვის.
        /// </summary>
        /// <param name="comment">შესამუშავებელი კომენტარის ინფორმაცია.</param>
        /// <param name="postid">პოსტის ID, რომელთანაც ასოცირდება კომენტარი.</param>
        /// <param name="userid">კომენტარის შემქმნელი მომხმარებლის ID.</param>
        [HttpPost("Comments/{postid}"), Authorize]
        public async Task<IActionResult> CreateComment(CommentRequestModel comment, int postid, int userid)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value; //JWT id ჩეკავს
            var JWTRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value; //JWT Role


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var post = await _context.Posts.Include(u => u.User).FirstOrDefaultAsync(u => u.PostId == postid);


            if (post == null)
            {
                return NotFound(); // Handle if the post doesn't exist
            }


            var user = await _context.Users.Include(u => u.Posts).FirstOrDefaultAsync(u => u.UserId == userid);


            if (user == null)
            {
                return NotFound(); // Handle if the post doesn't exist
            }

            if (userId != user.UserId.ToString())
            {
                if (JWTRole != "admin")
                {
                    return BadRequest("Authorize invalid");
                }
            }

            var NewComment = new CommentModel
            {
                Content = comment.Content,
                Picture = comment.Picture,
                Video = comment.Video,
                CreatedAt = DateTime.Now,
                Post = post,
                User = user,
            };

            if (post.User.UserId != userid) // Avoid sending notifications to the comment author
            {
                var notification = new NotificationModel
                {
                    Message = $"{user.UserName} დატოვა კომენტარი თქვენს პოსტზე: {post.Title}",
                    CreatedAt = DateTime.Now,
                    IsRead = false,
                    User = post.User
                };

                _context.Notifications.Add(notification);
            }


            _context.Comments.Add(NewComment);
            await _context.SaveChangesAsync();


            return Ok(NewComment);

        }


        /// <summary>
        /// მიიღეთ კომენტარები კონკრეტული პოსტისთვის.
        /// </summary>
        /// <param name="postId">პოსტის ID კომენტარის მისაღებად.</param>
        [HttpGet("Comments/{postId}")]
        public async Task<IActionResult> GetComments(int postId)
        {

            var comments = await _context.Comments.Where(c => c.Post.PostId == postId).Include(p => p.User).ToListAsync();

            var responseList = new List<object>();


            foreach (var comment in comments)
            {
                var postResponse = new
                {
                    commentid = comment.CommentId,
                    content = comment.Content,
                    video = comment.Video,
                    picture = comment.Picture,
                    createdAt = comment.CreatedAt,
                    user = new
                    {
                        userId = comment.User.UserId,
                        username = comment.User.UserName,
                        picture = comment.Picture,
                    },
                };

                responseList.Add(postResponse);
            }

            return Ok(responseList);

        }



        /// <summary>
        /// წაშალეთ კომენტარი მისი ID-ით და მომხმარებლის ID-ით.
        /// </summary>
        /// <param name="commentId">წაშლილი კომენტარის ID.</param>
        /// <param name="userId">კომენტარს წაშლის მომხმარებლის ID.</param>
        [HttpDelete("Comments/{commentId}"), Authorize]
        public async Task<IActionResult> DeleteComments(int commentId, int userId)
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value; //JWT id ჩეკავს
            var JWTRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value; //JWT Role

            var comment = await _context.Comments.Include(p => p.User).FirstOrDefaultAsync(p => p.CommentId == commentId);

            if (comment.User.UserId != userId)
            {
                return NotFound();
            }

            if (UserId != userId.ToString())
            {
                if (JWTRole != "admin")
                {
                    return BadRequest("Authorize invalid");
                }
            }


            // Remove associated notifications
            var relatedNotifications = await _context.Notifications
                .Where(n => n.User.UserId == userId && n.Message.Contains($"comment {comment.CommentId}"))
                .ToListAsync();

            _context.Notifications.RemoveRange(relatedNotifications);
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return Ok("Removed Successfully");
        }

        /// <summary>
        /// არსებული კომენტარის რედაქტირება.
        /// </summary>
        /// <param name="EditedComment">რედაქტირებული კომენტარის ინფორმაცია.</param>
        [HttpPut("Comments"), Authorize]
        public async Task<IActionResult> EditCommentar(EditCommentReqeustModel EditedComment)
        {


            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value; //JWT id ჩეკავს
            var JWTRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value; //JWT Role


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = await _context.Comments.Include(u => u.User).FirstOrDefaultAsync(u => u.CommentId == EditedComment.CommentId);  //მაძლევს Users Commentars

            if (comment == null)
            {
                return NotFound("Post Not Found");
            }


            var user = await _context.Users.Include(u => u.Posts).FirstOrDefaultAsync(u => u.UserId == comment.User.UserId);  //მაძლევს Users პოსტებით



            if (user == null)
            {
                return BadRequest("User Not Found");
            }

            if (userId != user.UserId.ToString() || JWTRole != "admin")
            {
                return BadRequest("Authorize invalid");
            }


            user.Comments.Remove(comment);



            comment.Content = EditedComment.Content;
            comment.Video = EditedComment.Video;
            comment.Picture = EditedComment.Picture;
            comment.CreatedAt = DateTime.Now;
            comment.User = user;



            user.Comments ??= new List<CommentModel>();
            user.Comments.Add(comment);

            await _context.SaveChangesAsync();



            return Ok(comment);
        }


        // --------Notifications




        /// <summary>
        /// მიიღეთ შეტყობინებები კონკრეტული მომხმარებლისთვის.
        /// </summary>
        /// <param name="userId">მომხმარებლის ID, რომლისთვისაც უნდა მიიღოთ შეტყობინებები.</param>
        [HttpGet("Notifications/{userId}"), Authorize]
        public async Task<IActionResult> GetUserNotifications(int userId)
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value; //JWT id ჩეკავს
            var JWTRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value; //JWT Role

            var user = await _context.Users.Include(u => u.Notifications).FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            if (UserId != userId.ToString())
            {
                if (JWTRole != "admin")
                {
                    return BadRequest("Authorize invalid");
                }
            }

            var notifications = user.Notifications.OrderByDescending(n => n.CreatedAt).ToList();
            return Ok(notifications);
        }

        /// <summary>
        /// მონიშნეთ შეტყობინება წაკითხულად მისი ID-ით.
        /// </summary>
        /// <param name="notificationId">შეტყობინებების ID, რომ მოინიშნოს წაკითხულად.</param>
        [HttpPut("Notifications/{notificationId}")]
        public async Task<IActionResult> MarkNotificationAsRead(int notificationId)
        {
            var notification = await _context.Notifications.FirstOrDefaultAsync(n => n.NotificationId == notificationId);

            if (notification == null)
            {
                return NotFound("Notification not found");
            }

            notification.IsRead = true;
            await _context.SaveChangesAsync();

            return Ok("Notification marked as read");
        }
    }
}
