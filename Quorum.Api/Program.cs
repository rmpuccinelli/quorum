using Microsoft.OpenApi.Models;
using Quorum.Application.Interfaces;
using Quorum.Application.Services;
using Quorum.Infrastructure;
using Quorum.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Configure data settings
var dataSettings = builder.Configuration.GetSection("DataSettings").Get<DataSettings>()
    ?? throw new InvalidOperationException("DataSettings section is missing in configuration");

var dataPath = Path.Combine(builder.Environment.ContentRootPath, dataSettings.FolderPath);
Directory.CreateDirectory(dataPath);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { 
        Title = "Quorum Legislative Analysis API", 
        Version = "v1",
        Description = "API for analyzing legislative data and voting patterns"
    });
});

builder.Services.AddScoped<IQuorumService, QuorumService>();
builder.Services.AddInfrastructure();

// Register configuration
builder.Services.Configure<DataSettings>(builder.Configuration.GetSection("DataSettings"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCors", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Quorum API V1");
        c.RoutePrefix = string.Empty; // Serve Swagger UI at the root
    });
}

// Add redirect from /swagger to root
app.MapGet("/swagger", context =>
{
    context.Response.Redirect("/");
    return Task.CompletedTask;
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("DevCors");
app.MapControllers();

app.Run(); 