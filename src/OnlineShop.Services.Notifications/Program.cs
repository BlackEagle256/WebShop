using OnlineShop.BackgroundTasks;
using OnlineShop.Infrastructure;
using OnlineShop.Persistance;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddBackgroundTasks(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapGet("/", () => "Service Running ...");

app.Run();
