using Microsoft.EntityFrameworkCore;
using EF_Core_01.Models;
using System.Configuration;
using EF_Core_01.Extensions;
using EF_Core_01.Models.InMemory;
using EF_Core_01.Services.IRepositories;
using EF_Core_01.Services.Repositories;
using EF_Core_01.Services.IServices;
using EF_Core_01.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ModelContext>(opts =>
{
    opts.UseOracle(configuration.GetConnectionString("OrclDb"));
});
builder.Services.AddControllers().AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddTransient<ModelContext, ModelContext>();

builder.Services.AddSingleton<CategoryMemoryModel>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddMemoryCache();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.LoadDataToMemory<CategoryMemoryModel, ModelContext>((data, dbContext) =>
{
    new CategoryMemorySeedAsync().SeedAsync(data, dbContext).Wait();
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
