namespace NetCoreStartProject.Contracts.V1.Requests.Identity
{
    public class UserLoginRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}