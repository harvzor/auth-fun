using System.Text.Json;
using AuthFun.Shared;
using IdentityModel.Client;

var client = new HttpClient();
var discoveryDocumentResponse = await client.GetDiscoveryDocumentAsync(SharedConstants.Urls.IdentityServer);

if (discoveryDocumentResponse.IsError)
{
    Console.WriteLine(discoveryDocumentResponse.Error);
    return;
}

var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
{
    Address = discoveryDocumentResponse.TokenEndpoint,
    ClientId = SharedConstants.AuthFunConsoleClient.ClientId,
    ClientSecret = SharedConstants.AuthFunConsoleClient.ClientSecret,
    Scope = SharedConstants.Scopes.AuthFunApiScope,
});

if (tokenResponse.IsError)
{
    Console.WriteLine(tokenResponse.Error);
    Console.WriteLine(tokenResponse.ErrorDescription);
    return;
}

Console.WriteLine(tokenResponse.AccessToken);

var apiClient = new HttpClient();
apiClient.SetBearerToken(tokenResponse.AccessToken!);

var response = await apiClient.GetAsync($"{SharedConstants.Urls.Api}/identity");

if (!response.IsSuccessStatusCode)
{
    Console.WriteLine(response.StatusCode);
}
else
{
    var jsonDocument  = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
    Console.WriteLine(JsonSerializer.Serialize(jsonDocument.RootElement, new JsonSerializerOptions
    {
        WriteIndented = true,
    }));
}
