
using bulkbuy.api.Data;
using bulkbuy.api.Services;
using bulkbuy.api.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories and services
builder.Services.AddScoped<IUserRepository, UserRepository>(); 
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IGroupService, GroupService>(); 

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173") // Frontend development server
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

//Automatic migration set up
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DataContext>();
    context.Database.Migrate();
}

app.UseRouting();
app.UseCors();
app.UseAuthorization(); 
app.MapControllers();
app.Run();


