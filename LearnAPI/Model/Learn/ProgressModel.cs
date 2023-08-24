using LearnAPI.Model.Learn.Test;
using LearnAPI.Model.User;
using System.ComponentModel.DataAnnotations;

namespace LearnAPI.Model.Learn
{
    public class ProgressModel
    {
        [Key]
        public int ProgressId { get; set; }

        public int UserId { get; set; }
        public virtual UserModel? User { get; set; }

        public int? CurrentLevelId { get; set; }
        public int? LastCompletedCourseId { get; set; }
        public int? LastCompletedSubjectId { get; set; }
        public int? LastCompletedTestId { get; set; }

        public virtual LevelModel? CurrentLevel { get; set; }
        public virtual CourseModel? LastCompletedCourse { get; set; }
        public virtual SubjectModel? LastCompletedSubject { get; set; }
        public virtual TestModel? LastCompletedTest { get; set; }
    }
}
