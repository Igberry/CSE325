using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MovieDbContext>(options =>
    options.UseSqlite("Data Source=movies.db"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
    context.Database.EnsureCreated();

    if (!context.Movies.Any())
    {
        context.Movies.AddRange(
            new Movie { Title = "The Matrix", Genre = "Science Fiction", ReleaseYear = 1999, Rating = 8.7m, Director = "The Wachowskis" },
            new Movie { Title = "Back to the Future", Genre = "Adventure", ReleaseYear = 1985, Rating = 8.5m, Director = "Robert Zemeckis" },
            new Movie { Title = "The Princess Bride", Genre = "Fantasy", ReleaseYear = 1987, Rating = 8.0m, Director = "Rob Reiner" }
        );
        context.SaveChanges();
    }
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Movies}/{action=Index}/{id?}");

app.Run();
