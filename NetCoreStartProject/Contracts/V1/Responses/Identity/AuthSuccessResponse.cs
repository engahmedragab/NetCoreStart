namespace NetCoreStartProject.Contracts.V1.Responses.Identity
{
    public class AuthSuccessResponse
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }

        public string ConfirmationMail { get; set; }
    }
}