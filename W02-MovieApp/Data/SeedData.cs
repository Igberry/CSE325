using MovieApp.Models;

namespace MovieApp.Data;

public static class SeedData
{
    public static void Initialize(MovieDbContext context)
    {
        context.Database.EnsureCreated();

        if (context.Movies.Any())
        {
            return;
        }

        context.Movies.AddRange(
            new Movie
            {
                Title = "The Dark Knight",
                Genre = "Action",
                ReleaseYear = 2008,
                Rating = 8.9m,
                Director = "Christopher Nolan"
            },
            new Movie
            {
                Title = "Interstellar",
                Genre = "Science Fiction",
                ReleaseYear = 2014,
                Rating = 8.7m,
                Director = "Christopher Nolan"
            },
            new Movie
            {
                Title = "The Lord of the Rings: The Fellowship of the Ring",
                Genre = "Fantasy",
                ReleaseYear = 2001,
                Rating = 8.8m,
                Director = "Peter Jackson"
            }
        );

        context.SaveChanges();
    }
}
