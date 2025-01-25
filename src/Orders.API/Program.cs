using Orders.API.Configurations;
using Orders.API.Endpoints;
using Orders.Application;
using Orders.Infrastructure;
using SharedLib.Tokens.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationConfig();
builder.Services.AddInfra(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);
builder.AddDocumentationConfig();
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddOpenApi();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthConfiguration();
app.MapEndpoints();

app.Run();