﻿using LearnAPI.Model.Learn;
using LearnAPI.Model.Social;
using System.ComponentModel.DataAnnotations;

namespace LearnAPI.Model.User
{
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }

        [StringLength(50, MinimumLength = 4, ErrorMessage = "Username must be between 4 and 50 characters.")]
        public string? UserName { get; set; }
        [StringLength(50)]
        public string? FirstName { get; set; }
        [StringLength(50)]
        public string? LastName { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        public string? Picture { get; set; }

        public byte[] PasswordHash { get; set; } = new byte[32];

        public byte[] PasswordSalt { get; set; } = new byte[32];

        public string? VerificationToken { get; set; }

        public DateTime VerifiedAt { get; set; }

        public string? PasswordResetToken { get; set; }

        public DateTime? ResetTokenExpires { get; set; }

        public string? Role { get; set; }

        // OAuth-specific properties
        public string? OAuthProvider { get; set; } // Store the OAuth provider (e.g., "Google")
        public string? OAuthProviderId { get; set; } // Store the unique identifier provided by the OAuth provider


        //Learn
        public virtual ICollection<CourseEnrollmentModel> Enrollments { get; set; } = new List<CourseEnrollmentModel>(); //კურსები რომელსაც ერთროულად გადის მაგალიტათ Javas და C# კურსი აქვს ერთროულად დაწყებული


        //Posts

        public ICollection<NotificationModel>? Notifications { get; set; } 
        public virtual ICollection<PostModel>? Posts { get; set; }
        public virtual ICollection<CommentModel>? Comments { get; set; }


        //Progress

        public virtual ProgressModel? Progress { get; set; }
        public DateTime LastActivity { get; set; }

    }
}
