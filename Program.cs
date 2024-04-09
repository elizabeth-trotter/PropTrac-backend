using PropTrac_backend.Services;
using PropTrac_backend.Services.Context;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<PasswordService>();
builder.Services.AddScoped<UserService>();

var connectionString = builder.Configuration.GetConnectionString("MyAppString");

// configures entity framework core to use SQL server as the database provider for a datacontext DbContext in our project
builder.Services.AddDbContext<DataContext>(Options => Options.UseSqlServer(connectionString));

builder.Services.AddCors(options => options.AddPolicy("PropTracPolicy", 
    builder => {
        builder.WithOrigins("http://localhost:5280", "http://localhost:3000", "https://prop-trac.vercel.app")
        .AllowAnyHeader()
        .AllowAnyMethod();
    }));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseCors("PropTracPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
