using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using TescoSW.DependencyInjection;
using URDemo.Server.Data;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddDbContext<DataContext>();

//Registrace testovacího BLManageru
builder.Services.AddTestEFServerBLManager<DataContext>(options => {
    options.SearchedAssemblies = new[] { typeof(DataContext).Assembly };
    options.SearchedNamespaces = new[] { "URDemo.Server.Models" };
});

var mvcBuilder = builder.Services.AddControllersWithViews().AddControllersAsServices();
//Registrace testovacího API
builder.Services.AddTestEFServerWebApi(mvcBuilder);
builder.Services.AddHttpContextAccessor();

builder.Services.AddRazorPages();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(); // /swagger/index.html
builder.Services.AddSwaggerGen(s => s.EnableAnnotations()); // /swagger/index.html

var app = builder.Build();

//Konfigurace testovacího BLManageru
app.UseTestEFServerBLManager();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpMethodOverride();
app.UseResponseCompression();

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<DataContext>();
    //context.Database.Migrate();
    context.Database.EnsureCreated();
}

app.Run();
