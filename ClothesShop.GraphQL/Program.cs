using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddGraphQLServer()
    .AddQueryType<DbLoggerCategory.Query>();

var app = builder.Build();

app.MapGraphQL();
app.UseRouting();
app.Run();