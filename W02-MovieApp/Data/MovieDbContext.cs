using Microsoft.EntityFrameworkCore;
using MovieApp.Models;

namespace MovieApp.Data;

public class MovieDbContext : DbContext
{
    public MovieDbContext(DbContextOptions<MovieDbContext> options)
        : base(options)
    {
    }

    public DbSet<Movie> Movies => Set<Movie>();
}
