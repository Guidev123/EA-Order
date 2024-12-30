using Orders.API.Configurations;
using Orders.API.Endpoints;
using Orders.Application;
using Orders.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationConfig();
builder.Services.AddInfra();
builder.Services.AddApplication(builder.Configuration);
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