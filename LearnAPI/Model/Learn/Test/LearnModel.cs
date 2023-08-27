using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LearnAPI.Model.Learn.Test
{
    public class LearnModel
    {
        [Key]
        public int LearnId { get; set; }

        [Required]
        public string? LearnName { get; set; }

        [Required]
        public string? Description { get; set; }


        public int VideoId { get; set; }
        public virtual VideoModel Video { get; set; } = null!;


        [ForeignKey("TestId")]
        public virtual TestModel Test { get; set; } = null!;

        public int SubjectId { get; set; }
        public virtual SubjectModel? Subject { get; set; }


    }
}
