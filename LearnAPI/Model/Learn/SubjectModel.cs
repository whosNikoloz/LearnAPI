using LearnAPI.Model.Learn.Test;
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
        public virtual CourseModel? Course { get; set; }

        public virtual ICollection<LearnModel> LearnMaterials { get; set; } = new List<LearnModel>();
        public virtual ICollection<TestModel> Tests { get; set; } = new List<TestModel>();
    }
}
