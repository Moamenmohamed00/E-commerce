using ECommerce.Infrastructure.Data.Seeding;
using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using ECommerce.Application;
using ECommerce.Infrastructure;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
builder.Services.AddApplication();        // MediatR, FluentValidation, Mapster
builder.Services.AddInfrastructure(builder.Configuration);  // DbContext, Identity, Services


var app = builder.Build();
if(app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
    await IdentitySeeder.SeedAsync(userManager, roleManager);
}

if (app.Environment.IsDevelopment()||app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();
app.Run();
