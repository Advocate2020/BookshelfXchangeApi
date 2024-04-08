using Batsamayi.Shared.API.Swagger;
using BookXChangeApi.Constants;
using BookXChangeApi.Util.Swagger;
using BookXChangeDB.Databases;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<BCXConstants>();
var serverVersion = ServerVersion.AutoDetect(BCXConstants.ConnectionString);

builder.Services.AddDbContextFactory<DatabaseContext>(dbContextOptions => { dbContextOptions.UseMySql(BCXConstants.ConnectionString, serverVersion); });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//specifies that everytime it is needed, a new instance will be created e.g.
//whenever someone hits the controller its gonna provide a fresh instance of the IUnitOfWork 

#region Swagger Setup

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.DocumentFilter<SwaggerTagFilter<BookshelfXCTags>>();

    c.AddBearerSecurityDefinition();
    c.AddBearerSecurityRequirement();
});

#endregion Swagger Setup

#region Authentication
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.Authority = $"https://securetoken.google.com/{BCXConstants.FirebaseID}";

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = $"https://securetoken.google.com/{BCXConstants.FirebaseID}",
            ValidateAudience = true,
            ValidAudience = BCXConstants.FirebaseID,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });
#endregion Authentication


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

#region Firebase Secrets

try
{

    FirebaseApp.Create(new AppOptions
    {
        Credential = GoogleCredential.FromFile(BCXConstants.FirebaseSecret)
    });
}
catch (Exception e)
{
    Console.WriteLine($"# Firebase setup failed : {e.Message}");
    throw;
}

#endregion Firebase Secrets

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Log "Application is starting" when the application starts
app.Lifetime.ApplicationStarted.Register(() =>
{
    Log.Information("Application Is Starting");
});

app.Run();
