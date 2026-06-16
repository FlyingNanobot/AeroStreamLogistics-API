using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// ------------------------------------------------------------
// 1. CORS (Allow your Blazor Server UI)
// ------------------------------------------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowUI", policy =>
    {
        policy.WithOrigins(
            "https://aerostreamlogistics-ui.dev.localhost:7272",
            "https://localhost:44335")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});

// ------------------------------------------------------------
// 2. Authentication (Microsoft Entra ID / Azure AD)
// ------------------------------------------------------------
var azureAd = builder.Configuration.GetSection("AzureAd");
var tenantId = azureAd["TenantId"];
var audience = azureAd["Audience"];
var instance = azureAd["Instance"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = $"{instance}{tenantId}/v2.0";
        options.Audience = audience;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = $"{instance}{tenantId}/v2.0",
            RoleClaimType = "roles"
        };
    });


// ------------------------------------------------------------
// 3. Authorization (Policies optional)
// ------------------------------------------------------------
builder.Services.AddAuthorization();

// ------------------------------------------------------------
// 4. Controllers (Global auth filter)
// ------------------------------------------------------------
builder.Services.AddControllers(options =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();

    options.Filters.Add(new AuthorizeFilter(policy));
});

// ------------------------------------------------------------
// 5. Swagger
// ------------------------------------------------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ------------------------------------------------------------
// 6. Dependency Injection
// ------------------------------------------------------------
builder.Services.AddSingleton<Repository.Contract.ITelemetryRepository, Repository.Concrete.TelemetryRepository>();
builder.Services.AddScoped<Business.Contract.ITelemetryService, Business.Concrete.TelemetryService>();

var app = builder.Build();

// ------------------------------------------------------------
// 7. Middleware Pipeline (Correct Order)
// ------------------------------------------------------------
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("AllowUI");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
