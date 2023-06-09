using Microsoft.OpenApi.Models;
using SCAGEUsers.Application.QuerySide;
using SCAGEUsers.Application.RepositorySide;
using SCAGEUsers.Application.Service;
using SCAGEUsers.Application.ServiceSide;
using SCAGEUsers.Infrastructure.Queries;
using SCAGEUsers.Infrastructure.Repository;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(opt => opt.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddScoped<IUserQuery, UserQueries>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "users API",
        Description = "Api builded using DDD pattern",
        Contact = new OpenApiContact
        {
            Name = "Tiago Lopes",
            Email = "saxtiago14@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/tiagolopesdev"),           
        }        
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(opt =>
{
    opt.RoutePrefix = string.Empty;
    opt.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
});

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
