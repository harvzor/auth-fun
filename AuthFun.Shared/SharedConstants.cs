namespace AuthFun.Shared;

public class SharedConstants
{
    public class Urls
    {
        public const string IdentityServer = "https://localhost:5001";
        public const string Api = "https://localhost:6001";
        public const string WebClient = "https://localhost:5002";
    }

    public class AuthFunConsoleClient
    {
        public const string ClientId = "console";
        public const string ClientSecret = "secret";
    }
    
    public class AuthFunWebClient
    {
        public const string ClientId = "web";
        public const string ClientSecret = "secret";
    }
    
    public class Scopes
    {
        public const string AuthFunApiScope = "AuthFun.Api";
    }
}
