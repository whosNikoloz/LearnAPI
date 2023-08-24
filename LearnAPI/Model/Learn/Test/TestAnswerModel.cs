using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LearnAPI.Model.Learn.Test
{
    public class TestAnswerModel
    {
        [Key]
        public int AnswerId { get; set; }
        public string? Option { get; set; }

        public bool IsCorrect { get; set; }

        [ForeignKey("TestId")]
        public virtual TestModel? Test { get; set; }
    }
}
