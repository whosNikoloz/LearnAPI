using System.ComponentModel.DataAnnotations;

namespace LearnAPI.Model.User
{
    public class CheckEmailRequest
    {
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
