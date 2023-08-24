using System.ComponentModel.DataAnnotations;

namespace LearnAPI.Model.Learn
{
    public class LevelModel
    {
        [Key]
        public int LevelId { get; set; }

        public string? LevelName { get; set; }

        public string? LogoURL { get; set; }

        public string? Description { get; set; }

        public virtual ICollection<CourseModel> Courses { get; set; } = new List<CourseModel>();
    }
}
