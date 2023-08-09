using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using ResourceInventory.Model;
using ResourceInventory.Model.CategoryModel;
using ResourceInventory.Service.CategoryService;
using ResourceInventory.Service.ProductService;
using ResourceInventory.Service.SubProductService;
using Swashbuckle.AspNetCore.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDBContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ResourceInventoryAPI",
        Version = "v1",
        Description = "ResourceInventoryAPI is a RESTful API that manages resources and inventory data. It provides endpoints to create, retrieve, update, and delete resources and inventory items."
    });//API Documentation

});
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ISubProductRepository, SubProductRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ResourceInventory V1");
        
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

