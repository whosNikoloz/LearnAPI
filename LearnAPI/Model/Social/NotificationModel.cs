using LearnAPI.Model.User;
using System.ComponentModel.DataAnnotations;

namespace LearnAPI.Model.Social
{
    public class NotificationModel
    {
        [Key]
        public int NotificationId { get; set; }
        public string? Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }
        public UserModel? User { get; set; }
    }
}
