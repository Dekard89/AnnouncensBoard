using AnnoucensBoard.Domain;
using AnnoucensBoard.Domain.Entity;
using AnnouncensBoard.Autoriztion.RequereHandlers;
using AnnouncensBoard.Autoriztion.Requerments;
using AnnouncensBoard.BLL.DTO;
using AnnouncensBoard.BLL.Interfaces;
using AnnouncensBoard.BLL.Services;
using AnnouncensBoard.BLL.Validation;
using AnnouncensBoard.DAL;
using AnnouncensBoard.Options;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging(options =>
{
    options.AddConsole();
    options.AddFile();
    
});
builder.Services.AddDataProtection();
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddDataProtection();
builder.Services.AddIdentityCore<IdentityUser>().AddEntityFrameworkStores<AppDbContext>().AddUserManager<UserManager<IdentityUser>>()
    .AddDefaultTokenProviders();

builder.Services.AddCors(options => options.AddPolicy("VueOrigin", policy =>
{
    policy.WithOrigins("http://localhost:8080", "http://localhost:443", "https://announcensboards:80")
    .SetIsOriginAllowedToAllowWildcardSubdomains()
    .AllowCredentials()
    .AllowAnyHeader()
    .WithMethods("GET", "POST", "PATCH", "DELETE", "OPTIONS")
    .SetPreflightMaxAge(TimeSpan.FromSeconds(3600));
}));

builder.Services.AddStackExchangeRedisCache(opt =>
{
    opt.Configuration = "redis:6379,abortConnect=false";
    opt.InstanceName = "AnnouncensBoard";
});


builder.Services.AddOptions<JwtOptions>()
    .BindConfiguration(JwtOptions.Section);
builder.Services.AddScoped<IRepository<Topic>, TopicStore>();
builder.Services.AddScoped<IValidator<RegisterRequest>, RegisterValidator>();
builder.Services.AddScoped<IValidator<TopicDTO>,TopicValidator>();
builder.Services.AddScoped<IValidator<SubjectDTO>,SubjectValidator>();
builder.Services.AddScoped<IValidator<CharacteristicDTO>, CharacteristicValidator>();
builder.Services.AddScoped<IValidator<LoginRequest>,LoginValidator>();
builder.Services.AddScoped<IMapper<Topic, TopicDTO>, TopicMapper>();
builder.Services.AddScoped<IMapper<Subject, SubjectDTO>, SubjectMapper>();
builder.Services.AddOptions<JwtOptions>()
    .BindConfiguration(JwtOptions.Section);


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

        };
    });

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
    .AddNegotiate();


builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = options.DefaultPolicy;
    options.AddPolicy("AdultOnlyPolicy", policy =>
    policy.Requirements.Add(new AgeRequerment(18)));
    options.AddPolicy("OnlyOwnerEditPolicy", policy =>
    policy.Requirements.Add(new OnlyOwnerRequerment()));
});

builder.Services.AddSingleton<IAuthorizationHandler, AgeHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, OnlyOwnerHandler>();


var app = builder.Build();

app.UseForwardedHeaders();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
 app.UseHttpsRedirection();


app.UseRouting();

app.UseCors("VueOrigin");

app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();


app.Run();
