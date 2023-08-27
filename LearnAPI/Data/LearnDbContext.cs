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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseEnrollmentModel>()
                .HasKey(ce => new { ce.UserId, ce.CourseId }); // Configure composite primary key

            modelBuilder.Entity<CourseEnrollmentModel>()
                .HasOne(ce => ce.User)
                .WithMany(u => u.Enrollments)
                .HasForeignKey(ce => ce.UserId); // Configure the User navigation property

            modelBuilder.Entity<CourseEnrollmentModel>()
                .HasOne(ce => ce.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(ce => ce.CourseId);

            modelBuilder.Entity<TestModel>()
                .HasOne(t => t.Subject)          // Specify the navigation property
                .WithMany(s => s.Tests)          // Specify the collection navigation property in SubjectModel
                .HasForeignKey(t => t.SubjectId) // Specify the foreign key property
                .IsRequired();

            modelBuilder.Entity<LearnModel>()
                .HasOne(l => l.Subject)
                .WithMany(s => s.LearnMaterials)
                .HasForeignKey(l => l.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);

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
