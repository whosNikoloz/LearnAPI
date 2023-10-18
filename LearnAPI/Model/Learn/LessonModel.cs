using LearnAPI.Model.Learn.Test;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LearnAPI.Model.Learn
{
    public class LessonModel
    {
        [Key]
        public int LessonId { get; set; }
        public string? LessonName { get; set; }
        public int SubjectId { get; set; }

        [JsonIgnore]
        public virtual SubjectModel? Subject { get; set; }

        [JsonIgnore]
        public virtual ICollection<ProgressModel> Progresses { get; set; } = new List<ProgressModel>();

        [JsonIgnore]
        public virtual ICollection<LearnModel> LearnMaterial { get; set; } = new List<LearnModel>();
    }
}
