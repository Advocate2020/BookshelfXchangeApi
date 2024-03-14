using BookXChangeDB.Databases;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = "server=localhost;user=root;password=Pass@word1;database=xchange;Connection Timeout=30;";

var serverVersion = ServerVersion.AutoDetect(connectionString);

builder.Services.AddDbContextFactory<DatabaseContext>(dbContextOptions => { dbContextOptions.UseMySql(connectionString, serverVersion); });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//specifies that everytime it is needed, a new instance will be created e.g.
//whenever someone hits the controller its gonna provide a fresh instance of the IUnitOfWork 

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Allows anybody to access the API
builder.Services.AddCors(o =>
{
    o.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});
builder.Services.AddControllers().AddNewtonsoftJson(op => op.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

//Add support to logging with SERILOG
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Add support to logging request with SERILOG
app.UseSerilogRequestLogging();
app.UseCors("AllowAll");
app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

// Log "Application is starting" when the application starts
app.Lifetime.ApplicationStarted.Register(() =>
{
    Log.Information("Application Is Starting");
});

app.Run();
