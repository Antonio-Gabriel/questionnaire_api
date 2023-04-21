using QuestionaryApp.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Database connection
string myConnection = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<DataContext>(
    options => options.UseSqlServer(myConnection)
);

// Logs manager
builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});
// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Scoped
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Logger.LogInformation("Adding Routes");
app.MapGet("/", () => "Getting started").WithTags("🔥 Starting");
app.Logger.LogInformation("Starting the app");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
