using EchoService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<DataModels.Interfaces.IEchoService>(new EchoService.Services.EchoService());

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(); // ASP.NET Core OpenAPI
builder.Services.AddOpenApiDocument(); // NSwag

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseOpenApi();
    app.UseSwaggerUi();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();
app.Map("/", (context) => Task.Run(() => context.Response.Redirect("/swagger")));

app.Run();
