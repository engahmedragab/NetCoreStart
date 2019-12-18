using System.Collections.Generic;

namespace NetCoreStartProject.Domain
{
    public class AuthenticationResult
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }

        public string ConfirmationEmailLink { get; set; }
        
        public bool Success { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }
}