using Microsoft.OpenApi.Models;
using SCAGEUsers.Application.RepositorySide;
using SCAGEUsers.Application.Service;
using SCAGEUsers.Application.ServiceSide;
using SCAGEUsers.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(opt => opt.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddControllers();
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
