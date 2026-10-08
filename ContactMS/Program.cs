using ContactMS.Business;
using ContactMS.Data;
using ContactMS.Data.Services;
using ContactMS.DTO.Mappers;
using ContactMS.Logging;
using ContactMS.Middleware;
using Microsoft.EntityFrameworkCore;
using ContactMS.Service.External;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Configure Serilog with SQL Server Sink (ContactLogDb)
var logConnectionString = builder.Configuration.GetConnectionString("ContactLogDb");

// adding column

var columnOptions = new ColumnOptions
{
    AdditionalColumns = new Collection<SqlColumn>
    {
        new SqlColumn("DateTimeUtc", SqlDbType.DateTime2)
    }
};

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .Enrich.With<UtcTimestampEnricher>()
    .WriteTo.Console()
    .WriteTo.MSSqlServer(
        connectionString: logConnectionString,
        sinkOptions: new MSSqlServerSinkOptions
        {
            TableName = "Logs",
            AutoCreateSqlTable = true
        },
        columnOptions: columnOptions,
        restrictedToMinimumLevel: LogEventLevel.Information)
    .CreateLogger();

builder.Host.UseSerilog();

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
builder.Services.AddHttpClient(); // need to add for communicate with other MS

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<ContactMSContext>(options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("ContactDb")
    )
);

builder.Services.AddEntities();
builder.Services.AddDataServices();
builder.Services.AddDTOMappers();
builder.Services.AddServices();
builder.Services.AddExternalService();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseSerilogRequestLogging();

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

