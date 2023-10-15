using LearnAPI.Model.Learn.Test;
using System.ComponentModel.DataAnnotations;
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

        public virtual ICollection<LearnModel> LearnMaterial { get; set; } = new List<LearnModel>();
    }
}
