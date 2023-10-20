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

        public int UserId { get; set; }

        public int CourseId { get; set; }

        public int SubjectId { get; set; }

        public int LessonId { get; set; }

        [JsonIgnore]
        public virtual UserModel? User { get; set; }

        [JsonIgnore]
        public virtual CourseModel? Course { get; set; }

        [JsonIgnore]
        public virtual SubjectModel? Subject { get; set; }

        [JsonIgnore]
        public virtual LessonModel? Lesson { get; set; }
    }
}
