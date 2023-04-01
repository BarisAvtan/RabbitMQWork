using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQWatermarkApp.Models;
using RabbitMQWatermarkApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddSingleton(sp => new ConnectionFactory() { Uri = new Uri(builder.Configuration.GetConnectionString("RabbitMQ")), DispatchConsumersAsync = true });

builder.Services.AddSingleton<RabbitMQClientService>();

builder.Services.AddSingleton<RabbitMQPublisher>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase(databaseName: "productDb");

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
