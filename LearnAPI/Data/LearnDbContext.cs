using LearnAPI.Model.Learn;
using LearnAPI.Model.Learn.Test;
using LearnAPI.Model.Social;
using LearnAPI.Model.User;
using Microsoft.EntityFrameworkCore;

namespace LearnAPI.Data
{
    public class LearnDbContext : DbContext
    {
        public LearnDbContext(DbContextOptions options) : base(options)
        {

        }


        //user

        public DbSet<UserModel> Users { get; set; }


        //Social
        public DbSet<PostModel> Posts { get; set; }
        public DbSet<CommentModel> Comments { get; set; }
        public DbSet<NotificationModel> Notifications { get; set; }


        //Learn 
        public DbSet<LevelModel> Levels { get; set; }
        public DbSet<CourseModel> Courses { get; set; }
        public DbSet<SubjectModel> Subjects { get; set; }
        public DbSet<CourseEnrollmentModel> CourseEnroll { get; set; }

        public DbSet<LearnModel> Learn { get; set; }
        public DbSet<TestModel> Tests { get; set; }
        public DbSet<TestAnswerModel> TestAnswers { get; set; }
        public DbSet<VideoModel> Videos { get; set; }

    }
}
