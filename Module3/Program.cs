using Application.Common.Interfaces.Authentication;
using Application.Services.Authentication;
using Domain.Interfaces;
using Infrastructure;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Adding Services 
// Authentication Services
{
    builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
    builder.Services.AddSingleton<IJWTTokenGenerator, JWTTokenGenerator>();
    builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(JwtSettings.SectionName));
    var jwtSettingsConf = new JwtSettings();
    builder.Configuration.Bind(JwtSettings.SectionName, jwtSettingsConf);
    builder.Services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "Module3",
        ValidAudience = "Module3",
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("a-very-long-super-secret-key-that-is-at-least-32-characters-long"))
    });
    builder.Services.AddAuthorization();
}
// Repository and UoW DI
{
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
}
// Other Services.
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddControllers();
}
// Swagger Services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            },
        },
        new string[]{}
    }
    });
});

// Add Problem Details Specification
builder.Services.AddProblemDetails(options =>
    options.CustomizeProblemDetails = context =>
    {
        // Add Trace ID from HTTPContext to individually identify exceptions.
        context.ProblemDetails.Extensions["traceId"] = context.HttpContext.TraceIdentifier;
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Global Error Handling
app.UseExceptionHandler("/error");
app.Map("/error", (HttpContext context) =>
{
    // Fetch the exception
    Exception? exception = context.Features.Get<IExceptionHandlerFeature>().Error;
    if (exception is null)
    {
        return Results.Problem();
    }

    // Custom Error Handling
    return Results.Problem(
        detail: exception.Message,
        title: exception.GetType().Name,
        statusCode: 500);
});
//app.UseAuthorization();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
