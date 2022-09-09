using ECommerse.API.Search.Interfaces;
using ECommerse.API.Search.Services;
using Polly;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var Configuration = builder.Configuration;
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddScoped<IProductsService, ProductsService>();

builder.Services.AddHttpClient("OrdersAPIService", config => {
    config.BaseAddress = new Uri(Configuration["Services:Orders"]);
});

builder.Services.AddHttpClient("ProductsAPIService", config => {
    config.BaseAddress = new Uri(Configuration["Services:Products"]);
}).AddTransientHttpErrorPolicy( p => p.WaitAndRetryAsync(5, _ =>  TimeSpan.FromMilliseconds(500) ));


//int retryCount = Convert.ToInt32(Configuration["RetryCount"]); // 3 from appsettings.json file

//var policy = Policy.Handle<WebException>()
//  .WaitAndRetryAsync(retryCount, _ => TimeSpan.FromMilliseconds(500));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
