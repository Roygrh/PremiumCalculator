using Microsoft.Extensions.Configuration;
using PremiumCalculator.Services;
using PremiumCalculator.Util;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

/*builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "_myAllowSpecificOrigins",
          policy =>
          {
              policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod(); // add the allowed origins  
          });
});*/

var serverSettings = builder.Configuration.GetSection("ServerSettings").Get<ServerSettings>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder.WithOrigins(serverSettings.ViewServerUrl)
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<FileService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors("AllowOrigin");

app.Run();
