using LearnAPI.Model.User;
using System.ComponentModel.DataAnnotations;

namespace LearnAPI.Model.Learn
{
    public class CourseModel
    {
        [Key]
        public int CourseId { get; set; }

        [Required]
        public string? CourseName { get; set; }

        public string? Description { get; set; }

        public int LevelId { get; set; } // Foreign key property

        public virtual LevelModel? Level { get; set; }

        public virtual ICollection<SubjectModel>? Subjects { get; set; }

        public virtual ICollection<CourseEnrollmentModel> Enrollments { get; set; } = new List<CourseEnrollmentModel>();
    }
}
