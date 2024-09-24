using AnnoucensBoard.Domain;
using AnnoucensBoard.Domain.Entity;
using AnnouncensBoard.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.StackExchangeRedis;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();



builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddScoped<IRepository<Topic>, TopicStore>();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddIdentityCore<IdentityUser>().AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddStackExchangeRedisCache(opt =>
    {
        opt.Configuration = "localhost";
        opt.InstanceName = "AnnouncensBoard";
    });
builder.Services.AddAuthorization(options =>
{
    
    options.FallbackPolicy = options.DefaultPolicy;
});

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
