using System.ComponentModel.DataAnnotations;

namespace LearnAPI.Model.User.LoginRequest
{
    public class UserLoginPhoneRequest
    {
        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;


        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
