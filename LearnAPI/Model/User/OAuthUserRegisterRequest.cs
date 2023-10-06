namespace LearnAPI.Model.User
{
    public class OAuthUserRegisterRequest
    {
        public string? email { get; set; }
        public string? username { get; set; }

        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? picture { get; set; }
        public string? oAuthProvider { get; set; }
        public string? oAuthProviderId { get; set; }
    }
}
