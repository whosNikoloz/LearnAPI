using LearnAPI.Model.User;
using System.Text.Json.Serialization;

namespace LearnAPI.Model.Learn
{
    public class CourseEnrollmentModel
    {
        public int UserId { get; set; }
        [JsonIgnore]
        public UserModel? User { get; set; }

        public int CourseId { get; set; }
        [JsonIgnore]
        public CourseModel? Course { get; set; }
    }
}
