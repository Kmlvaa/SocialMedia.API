
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SocialMediaAPİ.Automapper;
using SocialMediaAPİ.Common.Options;
using SocialMediaAPİ.DB;
using SocialMediaAPİ.DB.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var _cors = "allowAnyPolicy";

RegisterOptions();
RegisterDB();
RegisterAuth();
RegisterControllers();
RegisterServices();
RegisterCors();
RegisterSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(_cors);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

//Register Methods
void RegisterSwagger()
{
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "FAY API", Version = "v1" });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'"
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                },
                new string[] { }
            }
        });

    });
}

void RegisterControllers()
{
    builder.Services.AddControllers(options =>
    {
        var dateTimeBinder = options.ModelBinderProviders.FirstOrDefault(p => p.GetType() == typeof(DateTimeModelBinderProvider));
        if(dateTimeBinder != null)
            options.ModelBinderProviders.Remove(dateTimeBinder);

        options.Filters.Add<ValidateModelAttribute>();

    }).AddJsonOptions(jsonOptions =>
{
    jsonOptions.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

});
}
void RegisterDB()
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
}

void RegisterAuth()
{
    builder.Services.AddIdentity<User, UserRole>(options =>
    {
        options.SignIn.RequireConfirmedEmail = false;

        options.Password.RequiredLength = 6;
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;

        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.MaxFailedAccessAttempts = 5;
    })
   .AddEntityFrameworkStores<AppDbContext>()
   .AddDefaultTokenProviders();

    JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

    var jwtOptions = new JwtOptions();
    builder.Configuration.Bind(nameof(JwtOptions), jwtOptions);

    var key = Encoding.ASCII.GetBytes(jwtOptions.Key);

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(jwtBearerOptions =>
    {
        jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = jwtOptions.Issuer,
            ValidAudience = jwtOptions.Audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            NameClaimType = ClaimTypes.NameIdentifier,
        };
    });
}

void RegisterCors()
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(_cors, policy =>
        {
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.AllowAnyOrigin();
        });
    });
}

void RegisterServices()
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddHttpClient();
    builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

    builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
}

void RegisterOptions()
{
    builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
    //builder.Services.Configure<SeederOptions>(builder.Configuration.GetSection(nameof(SeederOptions)));
}
void MigrateDb()
{
    using (var scope = app.Services.CreateScope())
    {
        var dataContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dataContext.Database.Migrate();
    }
}