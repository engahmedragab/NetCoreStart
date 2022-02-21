namespace NetCoreStartProject.Contracts.V1.Requests.Identity
{
    public class RefreshTokenRequest
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }
    }
}