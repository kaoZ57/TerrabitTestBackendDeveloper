using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TerrabitTest.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddDbContext<mydatabaseContext>(builder =>
//{
//    builder.UseSqlServer("Server=localhost;Database=mydatabase;Trusted_Connection=False;MultipleActiveResultSets=true;"););
//}, ServiceLifetime.Scoped);

// Add services to the container.
builder.Services.AddSqlServer<mydatabaseContext>(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
