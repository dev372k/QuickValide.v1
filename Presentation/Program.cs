using API.Middlewares;
using ApiService;
using Presentation.Profiler;
using Serilog;
using Shared.Commons;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ServicesRegistry(builder.Configuration);
// Add services to the container.
Log.Logger = new LoggerConfiguration()
.MinimumLevel.Warning()
    .WriteTo.File(MiscilenousConstants.LOGPATH, rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Logging.ClearProviders();
builder.Logging.AddSerilog();
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
app.UseMiddleware<CustomMiddleware>();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.Run();
