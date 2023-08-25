using LearnAPI.Model.Learn.Test;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LearnAPI.Model.Learn.Request
{
    public class NewTestAnswerModel
    {

        [Required]
        public string Option { get; set; }

        public bool IsCorrect { get; set; }

        // Foreign Key
        public int TestId { get; set; }

    }
}
