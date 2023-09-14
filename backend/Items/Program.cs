global using Microsoft.EntityFrameworkCore;
using Items;
using Items.Data;
using Items.Repos;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHealthChecks();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddScoped<EfProductRepo>();
builder.Services.AddScoped<OrderRepo>();
builder.Services.AddScoped<UsersRepo>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddCors();
builder.Services.AddMassTransit(x =>
{
    var consumerAssembly = Assembly.GetExecutingAssembly();
    foreach (var type in consumerAssembly.GetTypes()
        .Where(t => t.Namespace != null && t.Namespace.StartsWith("Items.Consumers") && typeof(IConsumer).IsAssignableFrom(t)))
    {
        x.AddConsumer(type);
    }
    //x.UsingRabbitMq((context, cfg) =>
    //{
    //    cfg.UseMessageRetry(r =>
    //        r.Interval(2, TimeSpan.FromSeconds(1))
    //    );
    //    cfg.ConfigureEndpoints(context);
    //    //cfg.Host("rabbitmq://localhost");
    //    cfg.Host("rabbitmq://rabbitmq");
    //});
    x.UsingInMemory((context, cfg) =>
    {
        cfg.UseMessageRetry(r =>
            r.Interval(2, TimeSpan.FromSeconds(1))
        );

        cfg.ConfigureEndpoints(context);
    });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
        };
    });
var consumerAssembly = Assembly.GetExecutingAssembly();
foreach (var type in consumerAssembly.GetTypes()
    .Where(t => t.Namespace != null && t.Namespace.StartsWith("Items.Consumers") && typeof(IConsumer).IsAssignableFrom(t)))
{
    builder.Services.AddScoped(type);
}

builder.Services.AddScoped<HttpContextService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddLogging(loggingBuilder =>
    loggingBuilder.AddSerilog(dispose: true)
);

builder.Services.AddHttpContextAccessor();
var app = builder.Build();
app.MapHealthChecks("/healthz");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();
app.UseStaticFiles();
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ApplicationDbContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

app.Run();
