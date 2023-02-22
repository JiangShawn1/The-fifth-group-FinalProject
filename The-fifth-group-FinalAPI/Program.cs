using Microsoft.EntityFrameworkCore;
using The_fifth_group_FinalAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var TheFifthGroupOfTopicsConnectionString = builder.Configuration.GetConnectionString("TheFifthGroupOfTopics");
builder.Services.AddDbContext<TheFifthGroupOfTopicsContext>(options =>
	options.UseSqlServer(TheFifthGroupOfTopicsConnectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
