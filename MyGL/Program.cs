using Microsoft.EntityFrameworkCore;
using MyGL.Data;
using MyGL.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<MyGLContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("MyGLContext")));

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

// Connect to/create database
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
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message + " Retry: " + retryCount);
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

        // Add Default Rules to database
        foreach (var defaultRule in Constants.DefaultCategoryRules)
        {
            int id = db.Categories.Where(c => c.SubCategory == defaultRule.Item2).FirstOrDefault().Id;
            CategoryRule Rule = new()
            {
                SearchString = defaultRule.Item1,
                CategoryId = id
            };
            db.CategoryRules.Add(Rule);
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