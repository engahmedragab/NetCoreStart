namespace NetCoreStartProject.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";

        public const string Version = "v1";

        public const string Base = Root + "/" + Version;
        
        public static class Posts
        {
            public const string GetAll = Base + "/posts";

            public const string Update = Base + "/posts/{postId}";

            public const string Delete = Base + "/posts/{postId}";

            public const string Get = Base + "/posts/{postId}";

            public const string Create = Base + "/posts";
        }
        public static class Arrangements
        {
            public const string GetAll = Base + "/arrangements";

            public const string Update = Base + "/arrangements/{arrangementId}";

            public const string Delete = Base + "/arrangements/{arrangementId}";

            public const string Get = Base + "/arrangements/{postId}";

            public const string Create = Base + "/arrangements";
        }
        public static class Categories
        {
            public const string GetAll = Base + "/categories";

            public const string Update = Base + "/categories/{categorieId}";

            public const string Delete = Base + "/categories/{categorieId}";

            public const string Get = Base + "/categories/{categorieId}";

            public const string Create = Base + "/categories";
        }
        public static class Preparations
        {
            public const string GetAll = Base + "/preparations";

            public const string Update = Base + "/preparations/{preparationId}";

            public const string Delete = Base + "/preparations/{preparationId}";

            public const string Get = Base + "/preparations/{preparationId}";

            public const string Create = Base + "/preparations";
        }

        public static class Identity
        {
            public const string Login = Base + "/identity/login";

            public const string ExternalProvidersLogin = Base + "/identity/externallogin";

            public const string Register = Base + "/identity/register";

            public const string MailConfarm = Base + "/identity/mailconfarm";

            public const string Refresh = Base + "/identity/refresh";

            public const string EmailInUse = Base + "/identity/emailuse";

            public const string UserHasPassword = Base + "/identity/haspassword";

            public const string AddPassword = Base + "/identity/addpassword";

            public const string ChangePassword = Base + "/identity/changepassword";

            public const string ForgetPassword = Base + "/identity/forgetpassword";

            public const string ResetPassword = Base + "/identity/resetpassword";

            public const string ExternalProvidersLinkedinCallback = Version + "/auth/linkedin/callback";
            
        }
    }
}