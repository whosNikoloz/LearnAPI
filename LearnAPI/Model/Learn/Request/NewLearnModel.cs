using System.ComponentModel.DataAnnotations;

namespace LearnAPI.Model.Learn.Request
{
    public class NewLearnModel
    {

        [Required]
        public string? LearnName { get; set; }

        [Required]
        public string? Description { get; set; }

        public int VideoId { get; set; }
    }
}
