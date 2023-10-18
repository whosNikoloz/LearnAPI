using LearnAPI.Model.Learn.Test;
using LearnAPI.Model.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LearnAPI.Model.Learn
{
    public class ProgressModel
    {
        [Key]
        public int ProgressId { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }

        [ForeignKey("CourseId")]
        public int CourseId { get; set; }

        [ForeignKey("CurrentSubjectId")]
        public int CurrentSubjectId { get; set; }

        [ForeignKey("CurrentLessonId")]
        public int CurrentLessonId { get; set; }

        [JsonIgnore]
        public virtual UserModel? User { get; set; }

        [JsonIgnore]
        public virtual CourseModel? Course { get; set; }

        [JsonIgnore]
        public virtual SubjectModel? CurrentSubject { get; set; }

        [JsonIgnore]
        public virtual LessonModel? CurrentLesson { get; set; }
    }
}
