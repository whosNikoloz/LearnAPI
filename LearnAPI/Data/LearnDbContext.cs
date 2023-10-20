using LearnAPI.Model.Learn;
using LearnAPI.Model.Learn.Test;
using LearnAPI.Model.Social;
using LearnAPI.Model.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;

namespace LearnAPI.Data
{
    public class LearnDbContext : DbContext
    {
        public LearnDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User and Course Enrollment
            modelBuilder.Entity<CourseEnrollmentModel>()
                .HasKey(ce => new { ce.UserId, ce.CourseId });

            modelBuilder.Entity<CourseEnrollmentModel>()
                .HasOne(ce => ce.User)
                .WithMany(u => u.Enrollments)
                .HasForeignKey(ce => ce.UserId);

            modelBuilder.Entity<CourseEnrollmentModel>()
                .HasOne(ce => ce.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(ce => ce.CourseId);

            modelBuilder.Entity<ProgressModel>()
                .HasOne(p => p.Lesson)
                .WithMany()
                .HasForeignKey(p => p.LessonId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProgressModel>()
                .HasOne(p => p.Subject)
                .WithMany()
                .HasForeignKey(p => p.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);

        }


        //user

        public DbSet<UserModel> Users { get; set; }
        public DbSet<ProgressModel> Progress { get; set; }




        //Social
        public DbSet<PostModel> Posts { get; set; }
        public DbSet<CommentModel> Comments { get; set; }
        public DbSet<NotificationModel> Notifications { get; set; }


        //Learn 
        public DbSet<LevelModel> Levels { get; set; }
        public DbSet<CourseModel> Courses { get; set; }
        public DbSet<SubjectModel> Subjects { get; set; }
        public DbSet<CourseEnrollmentModel> CourseEnroll { get; set; }
        public DbSet<LessonModel> Lessons { get; set; }
        public DbSet<LearnModel> Learn { get; set; }
        public DbSet<TestModel> Tests { get; set; }
        public DbSet<TestAnswerModel> TestAnswers { get; set; }
        public DbSet<VideoModel> Videos { get; set; }

    }
}
