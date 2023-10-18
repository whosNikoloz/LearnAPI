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

            modelBuilder.Entity<SubjectModel>()
                .HasOne(s => s.Course)
                .WithMany(c => c.Subjects)
                .HasForeignKey(s => s.CourseId);

            modelBuilder.Entity<LessonModel>()
                .HasOne(l => l.Subject)
                .WithMany(s => s.Lessons)
                .HasForeignKey(l => l.SubjectId);

            modelBuilder.Entity<ProgressModel>()
                .HasOne(p => p.User)
                .WithMany(u => u.Progresses)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Restrict cascading deletes

            modelBuilder.Entity<ProgressModel>()
                .HasOne(p => p.Course)
                .WithMany(c => c.Progresses)
                .HasForeignKey(p => p.CourseId)
                .OnDelete(DeleteBehavior.Restrict); // Restrict cascading deletes

            modelBuilder.Entity<ProgressModel>()
                .HasOne(p => p.CurrentSubject)
                .WithMany()
                .HasForeignKey(p => p.CurrentSubjectId)
                .OnDelete(DeleteBehavior.Restrict); // Restrict cascading deletes

            modelBuilder.Entity<ProgressModel>()
                .HasOne(p => p.CurrentLesson)
                .WithMany()
                .HasForeignKey(p => p.CurrentLessonId)
                .OnDelete(DeleteBehavior.Restrict); // Restrict cascading deletes
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
