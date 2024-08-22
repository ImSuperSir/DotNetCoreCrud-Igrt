using System.Text;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskApi.Application.Services;
using TaskApi.Core.Interfaces;
using TaskApi.Infrastructure.Data;
using TaskApi.Infrastructure.Extensions;

// using TaskApi.Infrastructure.Extensions;
using TasksApi.Infrastructure.Extensions;
using TasksApi.Infrastructure.Repositories;
// using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddFluentValidationAutoValidation();
    //.AddFluentValidationAutoValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());




builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddLocalJwtAuthentication(builder.Configuration);
builder.Services.AddPersistence(builder.Configuration);



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();  // Add this line witn identity
app.UseAuthorization();

app.MapControllers();

app.Run();
