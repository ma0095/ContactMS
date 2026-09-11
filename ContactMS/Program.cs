using ContactMS.Data;
using Microsoft.EntityFrameworkCore;
using ContactMS.Business;
using ContactMS.DTO.Mappers;
using ContactMS.Data.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

string? connectionStrings = builder.Configuration.GetConnectionString("ContactDb");
builder.Services.AddDbContext<DbContext, ContactMSContext>(options => options.UseSqlServer(connectionStrings));


builder.Services.AddEntities();
builder.Services.AddDataServices();
builder.Services.AddDTOMappers();
builder.Services.AddServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
