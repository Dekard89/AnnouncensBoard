using AnnoucensBoard.Domain;
using AnnoucensBoard.Domain.Entity;
using AnnouncensBoard.BLL.Validation;
using AnnouncensBoard.DAL;
using AnnouncensBoard.Options;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using AnnouncensBoard.BLL.DTO;
using AnnouncensBoard.BLL.DTO.Subjects;
using AnnouncensBoard.BLL.Interfaces;
using AnnouncensBoard.BLL.Services;
using AnnouncensBoard.Autoriztion.Requerments;
using AnnouncensBoard.Autoriztion.RequereHandlers;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

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
builder.Services.AddIdentityCore<IdentityUser>().AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
    


builder.Services.AddStackExchangeRedisCache(opt =>
{
    opt.Configuration = "localhost";
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
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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

builder.Services.AddAuthentication();


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
