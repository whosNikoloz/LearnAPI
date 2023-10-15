using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnAPI.Model.Learn.Test
{
    public class TestModel
    {
        [Key]
        public int TestId { get; set; }


        [Required]
        public string? Question { get; set; }


        public string? Hint { get; set; }


        public virtual ICollection<TestAnswerModel>? Answers { get; set; }

    }
}
