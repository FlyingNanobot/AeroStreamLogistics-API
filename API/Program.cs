using Amazon;
using Amazon.S3;
using Business.Concrete;
using Business.Contract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using OpenSearch.Client;
using OpenSearch.Net;
using Repository.Concrete;
using Repository.Contract;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// ------------------------------------------------------------
// 1. CORS
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
// 2. Authentication (Azure AD)
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
// 3. Authorization
// ------------------------------------------------------------
builder.Services.AddAuthorization();

// ------------------------------------------------------------
// 4. Controllers
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
// 6. Database Clients (IMPORTANT)
// ------------------------------------------------------------

// Redis client
builder.Services.AddScoped<IRedisTelemetryService, RedisTelemetryService>();

// OpenSearch client
builder.Services.AddScoped<IOpenSearchTelemetryService, OpenSearchTelemetryService>();

// Postgres client
builder.Services.AddScoped<IPostgresTelemetryService, PostgresTelemetryService>();

// S3 client
builder.Services.AddScoped<IS3ArchiveService, S3ArchiveService>();

// ------------------------------------------------------------
// 7. Repository Layer
// ------------------------------------------------------------
builder.Services.AddSingleton<IRedisTelemetryRepository>(
    sp => new RedisTelemetryRepository(builder.Configuration["Redis:Connection"])
);
builder.Services.AddSingleton<IOpenSearchTelemetryRepository>(
    sp => new OpenSearchTelemetryRepository(builder.Configuration["OpenSearch:Url"])
);
builder.Services.AddSingleton<IPostgresTelemetryRepository>(
    sp => new PostgresTelemetryRepository(builder.Configuration["Postgres:ConnectionString"])
);
builder.Services.AddSingleton<IS3ArchiveRepository>(
    sp => new S3ArchiveRepository(
        builder.Configuration["S3:AccessKey"],
        builder.Configuration["S3:SecretAccessKey"],
        builder.Configuration["S3:BucketName"],
        builder.Configuration["S3:Region"]
    )
);


var app = builder.Build();

// ------------------------------------------------------------
// 8. Middleware Pipeline
// ------------------------------------------------------------
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("AllowUI");

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/health", () => "OK");

app.MapControllers();

app.Run();