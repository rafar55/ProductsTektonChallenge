using TektonChallenge.Core;
using TektonChallenge.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCore()
    .AddInfrastructure(builder.Configuration.GetConnectionString("DefaultConnection")!);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
