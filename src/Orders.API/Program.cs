using Orders.API.Configurations;
using Orders.API.Endpoints;
using Orders.Application;
using Orders.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfra();
builder.Services.AddApplication();
builder.AddDocumentationConfig();
builder.AddJwtConfiguration();
builder.Services.AddOpenApi();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseSecurity();
app.MapEndpoints();

app.Run();