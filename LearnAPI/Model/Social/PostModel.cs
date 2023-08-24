using LearnAPI.Model.User;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LearnAPI.Model.Social
{
    public class PostModel
    {
        [Key]
        public int PostId { get; set; }

        public string? Title { get; set; }

        public string? Subject { get; set; }

        public string? Content { get; set; }

        public string? Video { get; set; }

        public string? Picture { get; set; }

        public DateTime CreateDate { get; set; }

        [ForeignKey("UserId")]
        [JsonIgnore] // Ignore this property during serialization
        public virtual UserModel? User { get; set; } // Navigation property to UserModel

        public virtual ICollection<CommentModel> Comments { get; set; }
    }
}
