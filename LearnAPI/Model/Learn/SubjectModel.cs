using LearnAPI.Model.Learn.Test;
using System.ComponentModel.DataAnnotations;

namespace LearnAPI.Model.Learn
{
    public class SubjectModel
    {
        [Key]
        public int SubjectId { get; set; }

        public string? SubjectName { get; set; }

        public string? Description { get; set; }

        public string? LogoURL { get; set; }


        public int CourseId { get; set; }
        public virtual CourseModel? Course { get; set; }

        public virtual ICollection<TestModel>? Tests { get; set; }
    }
}
