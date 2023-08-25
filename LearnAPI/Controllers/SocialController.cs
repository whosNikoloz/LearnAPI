﻿using LearnAPI.Model.Social.Request;
using LearnAPI.Model.Social;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LearnAPI.Data;
using Microsoft.EntityFrameworkCore;
using LearnAPI.Model.Learn.Test;

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


        //Posts

        [HttpGet("Posts")]
        public async Task<ActionResult<IEnumerable<PostModel>>> GetPosts()
        {
            var posts = await _context.Posts.Include(p => p.User).Include(p => p.Comments).ToListAsync();

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
                    createdAt = post.CreateDate,
                    user = new
                    {
                        userId = post.User.UserId,
                        username = post.User.UserName,
                        picture = post.Picture,
                    },
                    comments = post.Comments
                };

                responseList.Add(postResponse);
            }

            return Ok(responseList);

        }

        [HttpGet("Post/{postId}")]
        public async Task<IActionResult> GetPost(int postId)
        {
            return Ok(await _context.Posts.FirstOrDefaultAsync(u => u.PostId == postId));

        }

        [HttpGet("Post/{subject}")]
        public async Task<IActionResult> PostsWithSubjeect(string subject)
        {
            return Ok(await _context.Posts.FirstOrDefaultAsync(u => u.Subject == subject));

        }


        [HttpPost("Posts/{Userid}")]
        public async Task<IActionResult> CreatePost(PostRequestModel Post, int Userid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _context.Users.Include(u => u.Posts).FirstOrDefaultAsync(u => u.UserId == Userid);  //მაძლევს Users პოსტებით

            if (user == null)
            {
                return BadRequest("User Not Found");
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

        [HttpPut("Posts")]
        public async Task<IActionResult> EditPost(EditPostRequestModel EditedPost)
        {
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

        [HttpDelete("Posts/{postId}")]
        public async Task<IActionResult> DeletePost(int postId)
        {
            var post = await _context.Posts.Include(u => u.User).FirstOrDefaultAsync(u => u.PostId == postId);  //მაძლევს Users პოსტებით

            if (post == null) { return NotFound(); }

            var user = await _context.Users.Include(u => u.Posts).FirstOrDefaultAsync(u => u.UserId == post.User.UserId);  //მაძლევს Users პოსტებით

            if (user == null) { return NotFound(); }

            user.Posts.Remove(post);


            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return Ok("Succesfully Deleted");
        }


        //Comments//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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



        [HttpPost("Comments/{postid}")]
        public async Task<IActionResult> CreateComment(CommentRequestModel comment, int postid, int userid)
        {
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




        [HttpDelete("Comments/{commentId}")]
        public async Task<IActionResult> DeleteComments(int commentId, int userId)
        {
            var comment = await _context.Comments.Include(p => p.User).FirstOrDefaultAsync(p => p.CommentId == commentId);

            if (comment.User.UserId != userId)
            {
                return NotFound();
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


        [HttpPut("Comments")]
        public async Task<IActionResult> EditCommentar(EditCommentReqeustModel EditedComment)
        {
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

        //Notifications//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        

        [HttpGet("Notifications/{userId}")]
        public async Task<IActionResult> GetUserNotifications(int userId)
        {
            var user = await _context.Users.Include(u => u.Notifications).FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var notifications = user.Notifications.OrderByDescending(n => n.CreatedAt).ToList();
            return Ok(notifications);
        }


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
