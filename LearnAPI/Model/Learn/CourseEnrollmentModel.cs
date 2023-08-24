using LearnAPI.Model.User;

namespace LearnAPI.Model.Learn
{
    public class CourseEnrollmentModel
    {
        public int UserId { get; set; }
        public UserModel? User { get; set; }

        public int CourseId { get; set; }
        public CourseModel? Course { get; set; }
    }
}
