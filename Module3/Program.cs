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
        ValidIssuer = jwtSettingsConf.Issuer,
        ValidAudience = jwtSettingsConf.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettingsConf.Secret))
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



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/error");
//app.UseAuthorization();
app.UseHttpsRedirection();

app.UseAuthentication();
app.MapControllers();

app.Run();
