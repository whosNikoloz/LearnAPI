using System.ComponentModel.DataAnnotations;

namespace LearnAPI.Model.Learn.Request
{
    public class NewTestModel
    {
        [Required]
        public string? Question { get; set; }


        public string? Hint { get; set; }


        public int SubjectId { get; set; }
    }
}
