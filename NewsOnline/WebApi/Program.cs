using BusinessLogic.Services;
using DataAccess;
using DataAccess.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<NewsContext>();
builder.Services.AddScoped<UnitOfWork, UnitOfWork>();
builder.Services.AddScoped<UserService, UserService>();
builder.Services.AddScoped<TagService, TagService>();
builder.Services.AddScoped<NewsService, NewsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();