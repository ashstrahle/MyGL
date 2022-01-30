using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyGL.Data;
using System.Net.NetworkInformation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<MyGLContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("MyGLContext"),
   sqlServerOptionsAction: sqlOptions =>
   {
       sqlOptions.EnableRetryOnFailure(
           maxRetryCount: 10,
           maxRetryDelay: TimeSpan.FromSeconds(30),
           errorNumbersToAdd: null);
   }));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using var loggerFactory = LoggerFactory.Create(loggingBuilder => loggingBuilder
    .SetMinimumLevel(LogLevel.Trace)
    .AddConsole());

ILogger logger = loggerFactory.CreateLogger<Program>();

// Apply Migrations
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MyGLContext>();

    SqlConnectionStringBuilder sqlbuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("MyGLContext"));
    // Retrieve the SQL server from connection string    
    string sqlserver = sqlbuilder.DataSource;

    // Check database server is up
    Ping ping = new();
    PingReply reply = ping.Send(sqlserver);
    int retryCount = 0;
    while (reply.Status != IPStatus.Success)
    {
        retryCount++;
        logger.LogInformation(message: "Ping database server failed. Retry: " + retryCount);
        Thread.Sleep(10000);
    }
    db.Database.Migrate();
}

// app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
