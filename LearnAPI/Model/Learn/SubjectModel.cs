using LearnAPI.Model.Learn.Test;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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

        [JsonIgnore]
        public virtual CourseModel Course { get; set; }

        public virtual ICollection<LessonModel> Lessons { get; set; } = new List<LessonModel>();
        public virtual ICollection<ProgressModel> Progresses { get; set; } = new List<ProgressModel>();
    }
}
