using AuthFun.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = "Cookies";
        options.DefaultChallengeScheme = "oidc";
    })
    .AddCookie("Cookies")
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = SharedConstants.Urls.IdentityServer;
        
        options.ClientId = SharedConstants.AuthFunWebClient.ClientId;
        options.ClientSecret = SharedConstants.AuthFunWebClient.ClientSecret;
        options.ResponseType = "code";
        
        options.Scope.Clear();
        options.Scope.Add("openid");
        options.Scope.Add("profile");
        options.GetClaimsFromUserInfoEndpoint = true;

        // Stop magic Microsoft code from renaming claims to ones they use with their internal services.
        options.MapInboundClaims = false;

        // Do not expose that we use dotnet core.
        options.DisableTelemetry = true;
        
        // Save tokens received from identity provider in the authentication cookie.  
        options.SaveTokens = true;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run(url: SharedConstants.Urls.WebClient);
