using Microsoft.EntityFrameworkCore;
using MyGL.Data;

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

// Apply Migrations
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MyGLContext>();

    // Retry connection to database until it's up
    int retryCount = 0;
    while (!db.Database.CanConnect())
    {
        retryCount++;
        Console.WriteLine("Connecting to database. Retry: " + retryCount);
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
