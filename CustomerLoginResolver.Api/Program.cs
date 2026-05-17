using CustomerLoginResolver.Api.Providers;
using CustomerLoginResolver.Api.Services;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<DocoProviderMock>();
builder.Services.AddScoped<LoyaltyProviderMock>();
builder.Services.AddScoped<LawProviderMock>();
builder.Services.AddScoped<LoginMethodResolver>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.MapGet("/", () => "Customer Login Resolver API is running");

app.MapGet("/api/login-method", async (
    string email,
    string market,
    LoginMethodResolver resolver) =>
{
    var result = await resolver.ResolveAsync(email, market);
    return Results.Ok(result);
})
.WithName("GetLoginMethod");

app.Run();