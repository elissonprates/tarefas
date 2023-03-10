using Api.Config;
using Application;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddDataBaseConfiguration();
builder.AddDependencyInjection();



builder.Services.AddAutoMapper(typeof(AppProfile));

builder.Services.AddControllers(config => config.Filters.Add<ServiceExceptionInterceptor>());

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicyTarefas", builder =>
        builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddHostedService<RabbitMQConsumer>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicyTarefas");

app.UseAuthorization();

app.MapControllers();

app.Run();
