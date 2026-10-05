using ContactMS.Business;
using ContactMS.Data;
using ContactMS.Data.Services;
using ContactMS.DTO.Mappers;
using Microsoft.EntityFrameworkCore;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5200", "http://localhost:4200")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
                      });
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

//string? connectionString = builder.Configuration.GetConnectionString("ContactDb");
//builder.Services.AddDbContext<ContactMSContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDbContext<ContactMSContext>(options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("ContactDb")
    )
);

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
app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
