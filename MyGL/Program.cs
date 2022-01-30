using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using MyGL.Data;
using MyGL.Models;

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

    int retryCount = 0;
    bool connected = false;
    while (!connected)
    {
        try
        {
            retryCount++;
            db.Database.Migrate();
            connected = true;
        }
        catch
        {
            logger.LogInformation(message: "No connection to database. Retry: " + retryCount);
            Thread.Sleep(10000);
            connected = false;
            if (retryCount > 10)
            {
                // Give up
                System.Environment.Exit(1);
            }
        }
    }
    // Add Default categories in there are none
    if (db.Categories.Count() == 0)
    {
        // Add Default Categories to database
        foreach (var defaultcategory in Constants.DefaultCategories)
        {
            Category category = new()
            {
                CategoryName = defaultcategory.Item1,
                SubCategory = defaultcategory.Item2
            };
            db.Categories.Add(category);
        }
        db.SaveChanges();

        // Add Default Conditions to database
        foreach (var defaultcondition in Constants.DefaultCategoryConditions)
        {
            int id = db.Categories.Where(c => c.SubCategory == defaultcondition.Item2).FirstOrDefault().Id;
            CategoryCondition condition = new()
            {
                SearchString = defaultcondition.Item1,
                CategoryId = id
            };
        }
        db.SaveChanges();
    }

    // app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapRazorPages();

    app.Run();
}