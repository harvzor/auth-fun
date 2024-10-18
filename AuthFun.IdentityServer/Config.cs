using AuthFun.Shared;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace AuthFun.IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
    [
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
    ];

    public static IEnumerable<ApiScope> ApiScopes =>
    [
        new ApiScope(name: SharedConstants.Scopes.AuthFunApiScope, displayName: SharedConstants.Scopes.AuthFunApiScope)
    ];

    public static IEnumerable<Client> Clients =>
    [
        new Client
        {
            ClientId = SharedConstants.AuthFunConsoleClient.ClientId,
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            ClientSecrets = 
            [
                new Secret(SharedConstants.AuthFunConsoleClient.ClientSecret.Sha512()),
            ],
            // Probably it would be better to use the audience to let the client access the AuthFun.Api server. Scopes
            // are more for specify which resources can be accessed.
            // > Scopes are a mechanism that allows the user to authorize a third-party application to perform only
            // > specific operations.
            // https://auth0.com/blog/id-token-access-token-what-is-the-difference/
            AllowedScopes = { SharedConstants.Scopes.AuthFunApiScope }
        },
        new Client
        {
            ClientId = SharedConstants.AuthFunWebClient.ClientId,
            ClientSecrets = { new Secret(SharedConstants.AuthFunWebClient.ClientSecret.Sha512()) },
            AllowedGrantTypes = GrantTypes.Code,
            RedirectUris = { $"{SharedConstants.Urls.WebClient}/signin-oidc" },
            PostLogoutRedirectUris = { $"{SharedConstants.Urls.WebClient}/signout-callback-oidc" },
            AllowedScopes =
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
            }
        }
    ];
}
