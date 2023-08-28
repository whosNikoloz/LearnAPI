namespace LearnAPI.Model.Learn.Request
{
    public class CompletionRequest
    {
        public int UserId { get; set; }
        public int? SubjectId { get; set; }
        public int? CourseId { get; set; }
        public int? LevelId { get; set; }
    }
}
