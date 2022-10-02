using RequestInterceptor;
using CustomAuthentification;
using Authentification.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
// Add services to the container
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddThresholdMiddleware();
builder.Services.AddCustomAuthorization();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// global cors policy
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.MapControllers();
app.UseMiddleware<BasicAuthMiddleware>();
app.UseMiddleware<ThresholdMiddleware>();

//TODO: Move to constants
app.Run("http://localhost:6054");
