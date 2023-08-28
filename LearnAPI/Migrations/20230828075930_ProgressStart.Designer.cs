﻿// <auto-generated />
using System;
using LearnAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LearnAPI.Migrations
{
    [DbContext(typeof(LearnDbContext))]
    [Migration("20230828075930_ProgressStart")]
    partial class ProgressStart
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LearnAPI.Model.Learn.CourseEnrollmentModel", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("CourseEnroll");
                });

            modelBuilder.Entity("LearnAPI.Model.Learn.CourseModel", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseId"));

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LevelId")
                        .HasColumnType("int");

                    b.HasKey("CourseId");

                    b.HasIndex("LevelId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("LearnAPI.Model.Learn.LevelModel", b =>
                {
                    b.Property<int>("LevelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LevelId"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LevelName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LogoURL")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LevelId");

                    b.ToTable("Levels");
                });

            modelBuilder.Entity("LearnAPI.Model.Learn.ProgressModel", b =>
                {
                    b.Property<int>("ProgressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProgressId"));

                    b.Property<int?>("CurrentLevelId")
                        .HasColumnType("int");

                    b.Property<int?>("LastCompletedCourseId")
                        .HasColumnType("int");

                    b.Property<int?>("LastCompletedSubjectId")
                        .HasColumnType("int");

                    b.Property<int?>("LastCompletedTestId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ProgressId");

                    b.HasIndex("CurrentLevelId");

                    b.HasIndex("LastCompletedCourseId");

                    b.HasIndex("LastCompletedSubjectId");

                    b.HasIndex("LastCompletedTestId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Progress");
                });

            modelBuilder.Entity("LearnAPI.Model.Learn.SubjectModel", b =>
                {
                    b.Property<int>("SubjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubjectId"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LogoURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubjectName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SubjectId");

                    b.HasIndex("CourseId");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("LearnAPI.Model.Learn.Test.LearnModel", b =>
                {
                    b.Property<int>("LearnId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LearnId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LearnName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.Property<int>("VideoId")
                        .HasColumnType("int");

                    b.HasKey("LearnId");

                    b.HasIndex("SubjectId");

                    b.ToTable("Learn");
                });

            modelBuilder.Entity("LearnAPI.Model.Learn.Test.TestAnswerModel", b =>
                {
                    b.Property<int>("AnswerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AnswerId"));

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("bit");

                    b.Property<string>("Option")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TestId")
                        .HasColumnType("int");

                    b.HasKey("AnswerId");

                    b.HasIndex("TestId");

                    b.ToTable("TestAnswers");
                });

            modelBuilder.Entity("LearnAPI.Model.Learn.Test.TestModel", b =>
                {
                    b.Property<int>("TestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TestId"));

                    b.Property<string>("Hint")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LearnId")
                        .HasColumnType("int");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.HasKey("TestId");

                    b.HasIndex("LearnId")
                        .IsUnique();

                    b.HasIndex("SubjectId");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("LearnAPI.Model.Learn.Test.VideoModel", b =>
                {
                    b.Property<int>("VideoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VideoId"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LevelId")
                        .HasColumnType("int");

                    b.Property<string>("VideName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VideoUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VideoId");

                    b.HasIndex("LevelId")
                        .IsUnique()
                        .HasFilter("[LevelId] IS NOT NULL");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("LearnAPI.Model.Social.CommentModel", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommentId"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Picture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PostId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("Video")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CommentId");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("LearnAPI.Model.Social.NotificationModel", b =>
                {
                    b.Property<int>("NotificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NotificationId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("NotificationId");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("LearnAPI.Model.Social.PostModel", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PostId"));

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Picture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Subject")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("Video")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("LearnAPI.Model.User.UserModel", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("LastActivity")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PasswordResetToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Picture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ResetTokenExpires")
                        .HasColumnType("datetime2");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("VerificationToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("VerifiedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LearnAPI.Model.Learn.CourseEnrollmentModel", b =>
                {
                    b.HasOne("LearnAPI.Model.Learn.CourseModel", "Course")
                        .WithMany("Enrollments")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LearnAPI.Model.User.UserModel", "User")
                        .WithMany("Enrollments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LearnAPI.Model.Learn.CourseModel", b =>
                {
                    b.HasOne("LearnAPI.Model.Learn.LevelModel", "Level")
                        .WithMany("Courses")
                        .HasForeignKey("LevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Level");
                });

            modelBuilder.Entity("LearnAPI.Model.Learn.ProgressModel", b =>
                {
                    b.HasOne("LearnAPI.Model.Learn.LevelModel", "CurrentLevel")
                        .WithMany()
                        .HasForeignKey("CurrentLevelId");

                    b.HasOne("LearnAPI.Model.Learn.CourseModel", "LastCompletedCourse")
                        .WithMany()
                        .HasForeignKey("LastCompletedCourseId");

                    b.HasOne("LearnAPI.Model.Learn.SubjectModel", "LastCompletedSubject")
                        .WithMany()
                        .HasForeignKey("LastCompletedSubjectId");

                    b.HasOne("LearnAPI.Model.Learn.Test.TestModel", "LastCompletedTest")
                        .WithMany()
                        .HasForeignKey("LastCompletedTestId");

                    b.HasOne("LearnAPI.Model.User.UserModel", "User")
                        .WithOne("Progress")
                        .HasForeignKey("LearnAPI.Model.Learn.ProgressModel", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CurrentLevel");

                    b.Navigation("LastCompletedCourse");

                    b.Navigation("LastCompletedSubject");

                    b.Navigation("LastCompletedTest");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LearnAPI.Model.Learn.SubjectModel", b =>
                {
                    b.HasOne("LearnAPI.Model.Learn.CourseModel", "Course")
                        .WithMany("Subjects")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("LearnAPI.Model.Learn.Test.LearnModel", b =>
                {
                    b.HasOne("LearnAPI.Model.Learn.SubjectModel", "Subject")
                        .WithMany("LearnMaterials")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("LearnAPI.Model.Learn.Test.TestAnswerModel", b =>
                {
                    b.HasOne("LearnAPI.Model.Learn.Test.TestModel", "Test")
                        .WithMany("Answers")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Test");
                });

            modelBuilder.Entity("LearnAPI.Model.Learn.Test.TestModel", b =>
                {
                    b.HasOne("LearnAPI.Model.Learn.Test.LearnModel", "Learn")
                        .WithOne("Question")
                        .HasForeignKey("LearnAPI.Model.Learn.Test.TestModel", "LearnId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LearnAPI.Model.Learn.SubjectModel", "Subject")
                        .WithMany("Tests")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Learn");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("LearnAPI.Model.Learn.Test.VideoModel", b =>
                {
                    b.HasOne("LearnAPI.Model.Learn.Test.LearnModel", "Learn")
                        .WithOne("Video")
                        .HasForeignKey("LearnAPI.Model.Learn.Test.VideoModel", "LevelId");

                    b.Navigation("Learn");
                });

            modelBuilder.Entity("LearnAPI.Model.Social.CommentModel", b =>
                {
                    b.HasOne("LearnAPI.Model.Social.PostModel", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId");

                    b.HasOne("LearnAPI.Model.User.UserModel", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId");

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LearnAPI.Model.Social.NotificationModel", b =>
                {
                    b.HasOne("LearnAPI.Model.User.UserModel", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("LearnAPI.Model.Social.PostModel", b =>
                {
                    b.HasOne("LearnAPI.Model.User.UserModel", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LearnAPI.Model.Learn.CourseModel", b =>
                {
                    b.Navigation("Enrollments");

                    b.Navigation("Subjects");
                });

            modelBuilder.Entity("LearnAPI.Model.Learn.LevelModel", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("LearnAPI.Model.Learn.SubjectModel", b =>
                {
                    b.Navigation("LearnMaterials");

                    b.Navigation("Tests");
                });

            modelBuilder.Entity("LearnAPI.Model.Learn.Test.LearnModel", b =>
                {
                    b.Navigation("Question");

                    b.Navigation("Video")
                        .IsRequired();
                });

            modelBuilder.Entity("LearnAPI.Model.Learn.Test.TestModel", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("LearnAPI.Model.Social.PostModel", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("LearnAPI.Model.User.UserModel", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Enrollments");

                    b.Navigation("Notifications");

                    b.Navigation("Posts");

                    b.Navigation("Progress");
                });
#pragma warning restore 612, 618
        }
    }
}
