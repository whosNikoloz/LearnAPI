using System.ComponentModel.DataAnnotations;

namespace LearnAPI.Model.Learn.Request
{
    public class NewCourseModel
    {
        [Required]
        public string? CourseName { get; set; }

        public string? Description { get; set; }

        public int LevelId { get; set; }
    }
}
