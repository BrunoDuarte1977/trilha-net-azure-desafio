using Microsoft.EntityFrameworkCore;
using TrilhaNetAzureDesafio.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<RHContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao"), sqlServerOptionsAction: sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
        maxRetryCount: 5,
        maxRetryDelay: TimeSpan.FromSeconds(30),
        errorNumbersToAdd: null);
    }));
  
    // builder.services.AddDbContext<RHContext>(options =>
    // options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao"), sqlServerOptionsAction: sqlOptions =>
    // {
    //     sqlOptions.EnableRetryOnFailure(
    //         maxRetryCount: 5,
    //         maxRetryDelay: TimeSpan.FromSeconds(30),
    //         errorNumbersToAdd: null);
    // }));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger
app.UseSwagger();
//app.UseSwaggerUI();
app.UseSwaggerUI(options  => {
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API V1");
    if(app.Environment.IsDevelopment())
        options.RoutePrefix  =  "swagger";
    else
        options.RoutePrefix  =  string.Empty;
    }
);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
