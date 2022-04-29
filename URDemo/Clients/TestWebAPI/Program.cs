using Microsoft.Extensions.DependencyInjection.Extensions;
using TescoSW.DependencyInjection;
using TescoSW.Net.Http;
using TescoSW.OW.Remoting.Http;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.TryAdd(ServiceDescriptor.Scoped<IHTTPCommunicator>((sc) => new SingleUserCommunicator(configuration.GetSection("HttpCommunicator"), null)));

builder.Services
       .AddScopedOWManager()
       .AddScopedOWAuthentication();

builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
